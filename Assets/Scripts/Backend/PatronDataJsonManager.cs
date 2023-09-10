#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using System.Linq;
using EasyButtons;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "PatronDataJsonManager", menuName = "ScriptableObjects/PatronDataJsonManager")]
public class PatronDataJsonManager : ScriptableObject{

    public List<PatronData> patrons;
    //public Dictionary<string, CharacterAnimatorData> animData;
    //public Dictionary<string, CharacterSpriteData> spriteData;
    
    [Button]
    public void GetAllPatronsDatas(){
        patrons = Resources.LoadAll<PatronData>("PatronData/").ToList();
        //animData = Resources.LoadAll<CharacterAnimatorData>("StepData/").ToDictionary(x=>x.name, x => x);
        //spriteData = Resources.LoadAll<CharacterSpriteData>("SpriteData/").ToDictionary(x=>x.name, x => x);
    }

    [Button]
    public void ExportAllAsJson(){
        foreach(PatronData patron in patrons){
            string data = EditorJsonUtility.ToJson(patron, true);
            if (string.IsNullOrEmpty(patron.name)) patron.name = patron.GetHashCode() + "";
                using (FileStream fs = new FileStream("Assets/Resources/PatronDataJSON/" + patron.name + ".json", FileMode.Create)){
                    using (StreamWriter writer = new StreamWriter(fs)){
                        writer.Write(data);
                        writer.Close();
                        writer.Dispose();
                    }
                fs.Close();
                fs.Dispose();
            }
        }
    }

    [Button]
    public void ImportAllAsJson(){
        TextAsset[] jsons = Resources.LoadAll<TextAsset>("PatronDataJSON/");
        foreach (var json in jsons){
            PatronData p = ScriptableObject.CreateInstance<PatronData>();
            EditorJsonUtility.FromJsonOverwrite(json.text, p);
            AssetDatabase.CreateAsset(p, "Assets/Resources/PatronData/" + p.name + ".asset");
        }
    }

}

#endif