using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "PatronData", menuName = "ScriptableObjects/PatronData")]
public class PatronData : ScriptableObject
{
    public CharacterSpriteData CharacterSpriteData;
    public CharacterAnimatorData CharacterAnimatorData;
    
    public Color[] body = new Color[3];
    public Color[] clothing = new Color[3];

    public string Name;
    public int Age;
    
    public float Patience;
    public float Chaos;
    public int Energy, Duration;
    
    public Bar BarRef;

    public Action Tick;
    
    public Patron Patron;
    
    public static PatronData RandomizePatronData(ref PatronData d, CharacterSpriteData sprite, CharacterAnimatorData anim) {
        d.CharacterSpriteData = sprite;
        d.CharacterAnimatorData = anim;
        d.body[0] = d.CharacterSpriteData.b1.Evaluate(Random.Range(0, 1));
        d.body[1] = d.CharacterSpriteData.b2.Evaluate(Random.Range(0, 1));
        d.body[2] = d.CharacterSpriteData.b3.Evaluate(Random.Range(0, 1));
        for(int i = 0; i < 3; i++)
            d.clothing[i] = Random.ColorHSV(0f, 1f, 0.2f, 0.8f, 0.2f, 0.9f);
        d.Patience = Random.Range(0, 1);
        d.Chaos = Random.Range(0, 1);
        d.Energy = Random.Range(1, 10);
        d.Duration = Random.Range(5, 10);
        return d;
    }

    
    public static PatronData GeneratePatronData(CharacterSpriteData sprite, CharacterAnimatorData anim) {
        PatronData d = ScriptableObject.CreateInstance<PatronData>();
        RandomizePatronData(ref d, sprite, anim);
        return d;
    }

    public Patron CreatePatron() {
        GameObject patronObject = GameObject.Instantiate(CharacterSpriteData.psb);
        patronObject.transform.localScale = Vector2.one * 0.1f;
        Patron = patronObject.AddComponent<Patron>();
        Patron.PatronData = this;
        Patron.Init();
        return Patron;
    }

    public void DestroyPatron() {
        Destroy(Patron);
    }
}