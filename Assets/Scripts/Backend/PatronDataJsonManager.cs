using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EasyButtons;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
[CreateAssetMenu(fileName = "PatronDataJsonManager", menuName = "ScriptableObjects/PatronDataJsonManager")]
public class PatronDataJsonManager : ScriptableObject{

    public List<PatronData> patrons;
    
    [Button]
    public void GetAllPatronsDatas(){
        patrons = Resources.LoadAll<PatronData>("PatronData/").ToList();
    }

    [Button]
    public void ExportAllAsJson(){
        foreach(PatronData patron in patrons){
            string data = JsonUtility.ToJson(patron);
            using (FileStream fs = new FileStream("Assets/Resources/PatronDataJSON/" + name + ".json", FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
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
            JsonUtility.FromJsonOverwrite(json.text, p);
            AssetDatabase.CreateAsset(p, "Assets/PatronData/" + p.name);
        }
    }

}