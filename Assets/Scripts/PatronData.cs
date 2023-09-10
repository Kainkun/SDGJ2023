using System;
using EasyButtons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "PatronData", menuName = "ScriptableObjects/PatronData")]
public class PatronData : ScriptableObject
{
    public CharacterSpriteData characterSpriteData;
    public CharacterAnimatorData characterAnimatorData;

    [Range(0, 1)]
    public float[] body_gradient = new float[3];
    public bool overrideGradient = false;
    public Color[] body = new Color[3];
    public Color[] clothing = new Color[3];

    public string name;
    public int age;
    public string[] discussion;
    
    public float patience;
    public int waitTime;
    public float chaos;
    public int energy;
    public int duration;

    [System.NonSerialized] public Bar barRef;
    [System.NonSerialized] public Patron patron;

    public static PatronData RandomizePatronData(ref PatronData d, CharacterSpriteData sprite, CharacterAnimatorData anim) {
        d.characterSpriteData = sprite;
        d.characterAnimatorData = anim;
        d.body[0] = d.characterSpriteData.b1.Evaluate(Random.Range(0, 1));
        d.body[1] = d.characterSpriteData.b2.Evaluate(Random.Range(0, 1));
        d.body[2] = d.characterSpriteData.b3.Evaluate(Random.Range(0, 1));
        for(int i = 0; i < 3; i++)
            d.clothing[i] = Random.ColorHSV(0f, 1f, 0.2f, 0.8f, 0.2f, 0.9f);
        d.patience = Random.Range(0, 1f);
        d.chaos = Random.Range(0, 1f);
        d.energy = Random.Range(1, 10);
        d.waitTime = Random.Range(8, 15) * 2;
        d.duration = Random.Range(40, 80) * 2; //Seconds
        return d;
    }
    
    public static PatronData GeneratePatronData(CharacterSpriteData sprite, CharacterAnimatorData anim) {
        PatronData d = ScriptableObject.CreateInstance<PatronData>();
        RandomizePatronData(ref d, sprite, anim);
        return d;
    }

    [Button]
    public Patron CreatePatron() {
        GameObject patronObject = GameObject.Instantiate(characterSpriteData.psb);
        if(name != null) patronObject.transform.name = name;
        patronObject.transform.localScale = Vector2.one * 0.1f;
        patron = patronObject.AddComponent<Patron>();
        patron.PatronData = this;
        patron.Init();
        return patron;
    }

    public void DestroyPatron() {
        Destroy(patron);
    }
    
    public void Tick() {
        duration -= 1;
        if(duration <= 0)
            barRef.Exit(this);
    }
}