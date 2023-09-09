using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEditor;
using UnityEngine;

public class PatronView
{
    private static CharacterSpriteData[] characterDatas;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        characterDatas = Resources.LoadAll<CharacterSpriteData>("SpriteData");
    }

    [MenuItem("Tests/Create Many Patrons")]
    public static void CreateMany()
    {
        for (int i = 0; i < characterDatas.Length; i++)
        {
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    CharacterSpriteData data = characterDatas[i];
                    GameObject o = CreatePatron(data);
                    o.transform.position = new Vector3((x * 2) + (i * 2 * 15), y * 2, 0);
                }
            }
        }
    }

    [MenuItem("Tests/Create Random Patron")]
    public static GameObject CreateRandomPatron()
    {
        return CreatePatron(characterDatas[Random.Range(0, characterDatas.Length)]);
    }

    public static GameObject CreatePatron(CharacterSpriteData characterData)
    {
        GameObject characterObject = Object.Instantiate(characterData.psb);
        characterObject.transform.localScale = Vector2.one * 0.1f;

        List<Color> pickedColors = new List<Color>();

        Transform t;
        t = characterObject.transform.Find("b1");
        if (t)
        {
            Color color = characterData.b1.Evaluate(Random.Range(0f, 1f));
            pickedColors.Add(color);
            t.GetComponent<SpriteRenderer>().color = color;
        }

        t = characterObject.transform.Find("b2");
        if (t)
        {
            Color color = characterData.b2.Evaluate(Random.Range(0f, 1f));
            pickedColors.Add(color);
            t.GetComponent<SpriteRenderer>().color = color;
        }

        t = characterObject.transform.Find("b3");
        if (t)
        {
            Color color = characterData.b3.Evaluate(Random.Range(0f, 1f));
            pickedColors.Add(color);
            t.GetComponent<SpriteRenderer>().color = color;
        }

        t = characterObject.transform.Find("c1");
        if (t)
        {
            Color color = Random.ColorHSV(0f, 1f, 0.2f, 0.8f, 0.2f, 0.9f);
            pickedColors.Add(color);
            t.GetComponent<SpriteRenderer>().color = color;
        }

        t = characterObject.transform.Find("c2");
        if (t)
        {
            Color color = Random.ColorHSV(0f, 1f, 0.2f, 0.8f, 0.2f, 0.9f);
            pickedColors.Add(color);
            t.GetComponent<SpriteRenderer>().color = color;
        }

        t = characterObject.transform.Find("c3");
        if (t)
        {
            Color color = Random.ColorHSV(0f, 1f, 0.2f, 0.8f, 0.2f, 0.9f);
            pickedColors.Add(color);
            t.GetComponent<SpriteRenderer>().color = color;
        }

        return characterObject;
    }
}