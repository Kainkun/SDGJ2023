using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Patron : MonoBehaviour
{
    public PatronData PatronData;
    public SpriteRenderer[] BodyRenderers = new SpriteRenderer[3];
    public SpriteRenderer[] ClothingRenderers = new SpriteRenderer[3];
    public CharacterAnimator CharacterAnimator;
    public float patienceRemaining;
    public Action OnLeave;
    
    public void Tick() {
        patienceRemaining -= 1;
        if (patienceRemaining <= 0) {
            OnLeave.Invoke();
        }
    }

    public void Leave()
    {

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
        
        CharacterAnimator = this.gameObject.AddComponent<CharacterAnimator>();
        CharacterAnimator.data = PatronData.characterAnimatorData;
        CharacterAnimator.MoveToWaitPosition();
        OnLeave += Leave;
        patienceRemaining = PatronData.waitTime;
    }
}
