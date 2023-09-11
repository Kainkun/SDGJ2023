using System;
using EasyButtons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "PatronData", menuName = "ScriptableObjects/PatronData")]
public class PatronData : ScriptableObject{

    #region Static Members

    private static string[][] _discussions;
    private static string[][] discussions{
        get{
            if(_discussions == null){
                TextAsset asset = Resources.Load<TextAsset>("Discussion");
                if (asset == null)
                    return _discussions;
                _discussions = JsonUtility.FromJson<string[][]>(asset.text);
            }
            return _discussions;
        }
    }
    
    private static string[] _names;
    private static string[] names{ get{
            if (_names == null){
                TextAsset asset = Resources.Load<TextAsset>("Names");
                if (asset == null)
                    return _names;
                _names = asset.text.Split("\n");
            }
            return _names;
        }
    }
    
    private static CharacterAnimatorData[] _characterAnimatorDatas;
    private static CharacterAnimatorData[] characterAnimatorDatas{
        get{
            if(_characterAnimatorDatas == null)
                _characterAnimatorDatas = Resources.LoadAll<CharacterAnimatorData>("StepData");
            return _characterAnimatorDatas;
        }
    }
    
    private static CharacterSpriteData[] _characterSpriteDatas;
    private static CharacterSpriteData[] characterSpriteDatas{
        get{
            if(_characterAnimatorDatas == null)
                _characterSpriteDatas = Resources.LoadAll<CharacterSpriteData>("SpriteData");
            return _characterSpriteDatas;
        }
    }

    #endregion
    
    public CharacterSpriteData characterSpriteData;
    public CharacterAnimatorData characterAnimatorData;

    [Range(0, 1)]
    public float[] body_gradient = new float[3];
    public bool overrideGradient = false;
    public Color[] body = new Color[3];
    public Color[] clothing = new Color[3];

    public bool overrideID;
    public PatronData ID;

    public string name;
    public int age;
    public string[] discussion;
    
    public float patience;
    public int waitTime;
    public float chaos;
    public int energy;
    public int duration;

    public Action<int> lineSpotChange;

    public bool glasses;
    public Vector3 glassesPosition;

    [System.NonSerialized] public Bar barRef;
    [System.NonSerialized] public Patron patron;

    public static PatronData RandomizePatronData(ref PatronData d){
        d.characterSpriteData = characterSpriteDatas[Random.Range(0, characterSpriteDatas.Length)];;
        d.characterAnimatorData = characterAnimatorDatas[Random.Range(0, characterAnimatorDatas.Length)];
        d.body[0] = d.characterSpriteData.b1.Evaluate(d.body_gradient[0] = Random.Range(0, 1f));
        d.body[1] = d.characterSpriteData.b2.Evaluate(d.body_gradient[1] = Random.Range(0, 1f));
        d.body[2] = d.characterSpriteData.b3.Evaluate(d.body_gradient[2] = Random.Range(0, 1f));
        if(discussions != null) d.discussion = discussions[Random.Range(0, discussions.Length)];
        if(names != null) d.name = names[Random.Range(0, names.Length)];
        for(int i = 0; i < 3; i++)
            d.clothing[i] = Random.ColorHSV(0f, 1f, 0.2f, 0.8f, 0.2f, 0.9f);
        d.patience = Random.Range(0, 1f);
        d.chaos = Random.Range(0, 1f);
        d.energy = Random.Range(1, 10);
        if (d.energy >= 7)
        {
            d.discussion = Dialogue.badDialogues[Random.Range(0, Dialogue.badDialogues.Length)];
        }
        else
        {
            d.discussion = Dialogue.goodDialogues[Random.Range(0, Dialogue.goodDialogues.Length)];
        }
        d.waitTime = Random.Range(8, 15) * 2;
        d.duration = Random.Range(40, 80) * 2; //Seconds
        d.age = Random.Range(16, 40);
        return d;
    }
    
    public static PatronData GeneratePatronData() {
        PatronData d = ScriptableObject.CreateInstance<PatronData>();
        RandomizePatronData(ref d);
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