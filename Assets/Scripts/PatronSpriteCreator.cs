using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEditor;
using UnityEngine;

public class PatronSpriteCreator
{
    private static PatronSpriteData[] patronDatas;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        patronDatas = Resources.LoadAll<PatronSpriteData>("SpriteData");
    }

    [MenuItem("Tests/Create Many Patrons")]
    public static void CreateMany()
    {
        for (int i = 0; i < patronDatas.Length; i++)
        {
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    PatronSpriteData data = patronDatas[i];
                    GameObject o = CreatePatron(data);
                    o.transform.position = new Vector3((x * 2) + (i * 2 * 15), y * 2, 0);
                }
            }
        }
    }

    [MenuItem("Tests/Create Random Patron")]
    public static GameObject CreateRandomPatron()
    {
        return CreatePatron(patronDatas[Random.Range(0, patronDatas.Length)]);
    }

    public static GameObject CreatePatron(PatronSpriteData patronData)
    {
        GameObject patronObject = Object.Instantiate(patronData.psb);
        patronObject.transform.localScale = Vector2.one * 0.1f;

        List<Color> pickedColors = new List<Color>();

        foreach (var sr in patronObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingLayerName = "Character";
        }

        Transform t;
        t = patronObject.transform.Find("b1");
        if (t)
        {
            Color color = patronData.b1.Evaluate(Random.Range(0f, 1f));
            pickedColors.Add(color);
            SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Character";
            sr.color = color;
        }

        t = patronObject.transform.Find("b2");
        if (t)
        {
            Color color = patronData.b2.Evaluate(Random.Range(0f, 1f));
            pickedColors.Add(color);
            SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Character";
            sr.color = color;
        }

        t = patronObject.transform.Find("b3");
        if (t)
        {
            Color color = patronData.b3.Evaluate(Random.Range(0f, 1f));
            pickedColors.Add(color);
            SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Character";
            sr.color = color;
        }

        t = patronObject.transform.Find("c1");
        if (t)
        {
            Color color = Random.ColorHSV(0f, 1f, 0.2f, 0.8f, 0.2f, 0.9f);
            pickedColors.Add(color);
            SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Character";
            sr.color = color;
        }

        t = patronObject.transform.Find("c2");
        if (t)
        {
            Color color = Random.ColorHSV(0f, 1f, 0.2f, 0.8f, 0.2f, 0.9f);
            pickedColors.Add(color);
            SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Character";
            sr.color = color;
        }

        t = patronObject.transform.Find("c3");
        if (t)
        {
            Color color = Random.ColorHSV(0f, 1f, 0.2f, 0.8f, 0.2f, 0.9f);
            pickedColors.Add(color);
            SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Character";
            sr.color = color;
        }

        return patronObject;
    }
}