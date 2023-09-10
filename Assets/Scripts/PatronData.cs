using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PatronData", menuName = "ScriptableObjects/PatronData")]
public class PatronData : ScriptableObject
{
    public CharacterSpriteData CharacterSpriteData;
    
    public Color[] body = new Color[3];
    public Color[] clothing = new Color[3];

    public int Patience;
    public float Chaos;
    public int Energy, Duration;
    
    public Bar BarRef;

    public Action Tick;
    
    public Patron Patron;

    public void CreatePatron() {
        GameObject patronObject = GameObject.Instantiate(CharacterSpriteData.psb);
        patronObject.transform.localScale = Vector2.one * 0.1f;
        Patron = patronObject.AddComponent<Patron>();
        Patron.PatronData = this;
        Patron.Init();
    }
}