using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Patron : MonoBehaviour
{
    public PatronData PatronData;
    public SpriteRenderer[] BodyRenderers;
    public SpriteRenderer[] ClothingRenderers;
    
    public Patron(int patience, float chaos, int energy, int duration) {
        PatronData.Patience = patience;
        PatronData.Chaos = chaos;
        PatronData.Energy = energy;
        PatronData.Duration = duration;
        PatronData.Tick = Tick;
        PatronData.Patron = this;
    }

    public Patron() : this(Random.Range(0, 100), 
                            Random.Range(0, 100),
                            Random.Range(0, 100),
                            Random.Range(0, 100))
    { }

    public void Tick() {
        PatronData.Duration -= 1;
    }

    public void Init() {
        foreach (var sr in this.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingLayerName = "Character";
        }

        Transform t;
        for (int i = 0; i < 3; i++)
        {
            t = this.transform.Find("b" + (i+1));
            if (t)
            {
                BodyRenderers[i] = t.GetComponent<SpriteRenderer>();
                BodyRenderers[i].sortingLayerName = "Character";
                BodyRenderers[i].color = PatronData.body[i];
            }
        }
        
        for (int i = 0; i < 3; i++)
        {
            t = this.transform.Find("c" + (i+1));
            if (t)
            {
                ClothingRenderers[i] = t.GetComponent<SpriteRenderer>();
                ClothingRenderers[i].sortingLayerName = "Character";
                ClothingRenderers[i].color = PatronData.clothing[i];
            }
        }
    }
}
