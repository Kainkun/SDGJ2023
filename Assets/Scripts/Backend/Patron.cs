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
    public int linePosition = 1;

    public List<int> sortingOrder = new List<int>();
    
    public void Tick(){
        if (linePosition != 0)
            return;

            patienceRemaining -= 1;
        if (patienceRemaining <= 0) {
            OnLeave.Invoke();
        }
    }

    public void OnLinePositionChange(int position){
        linePosition = position;
        CharacterAnimator.MoveToLinePosition(position);
        SetSortingLayer("Character", linePosition * 10);
    }

    public void SetSortingLayer(string s, int offset){
        int i = 0;
        foreach (var sr in this.GetComponentsInChildren<SpriteRenderer>()){
            sr.sortingLayerName = s;
            sr.sortingOrder = sortingOrder[i] + offset;
            i++;
        }
    }

    public void OnDestroy(){
        PatronData.lineSpotChange -= OnLinePositionChange;
    }

    public void Init(){
        foreach (var sr in this.GetComponentsInChildren<SpriteRenderer>()){
            sortingOrder.Add(sr.sortingOrder); 
            sr.sortingLayerName = "Character";
            char[] name = sr.name.ToCharArray();
            if (name.Length > 2)
                continue;
            int i = (int)Char.GetNumericValue(name[1]) - 1;
            switch(name[0]){
                case 'c':
                    ClothingRenderers[i] = sr;
                    ClothingRenderers[i].color = PatronData.clothing[i];
                    break;
                case 'b':
                    BodyRenderers[i] = sr;
                    BodyRenderers[i].color = PatronData.body[i];
                    break;
                default: 
                    break;
            }
        }

        if (PatronData.overrideGradient){
            if(BodyRenderers[0]) BodyRenderers[0].color = PatronData.body[0];
            if(BodyRenderers[1]) BodyRenderers[1].color = PatronData.body[1];
            if(BodyRenderers[2]) BodyRenderers[2].color = PatronData.body[2];
        } else {
            if(BodyRenderers[0]) BodyRenderers[0].color = PatronData.characterSpriteData.b1.Evaluate(PatronData.body_gradient[0]);
            if(BodyRenderers[1]) BodyRenderers[1].color = PatronData.characterSpriteData.b2.Evaluate(PatronData.body_gradient[1]);
            if(BodyRenderers[2]) BodyRenderers[2].color = PatronData.characterSpriteData.b3.Evaluate(PatronData.body_gradient[2]);
        }

        CharacterAnimator = this.gameObject.AddComponent<CharacterAnimator>();
        CharacterAnimator.data = PatronData.characterAnimatorData;
        PatronData.lineSpotChange += OnLinePositionChange;
        patienceRemaining = PatronData.waitTime;

        SetSortingLayer("Character", linePosition * 10);
    }
}
