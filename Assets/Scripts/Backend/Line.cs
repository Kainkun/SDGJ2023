using System;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Line : MonoBehaviour
{

    public LinkedList<PatronData> PatronDatas = new LinkedList<PatronData>();
    public List<PatronData> BakedPatrons = new List<PatronData>();
    public Bar BarRef;
    public Action OnTick;


    public UnityEvent<PatronData> OnInteract;
    public UnityEvent<PatronData> OnLeaveLine;

    public void Start() {
        MusicBeatSystem.Instance.OnBeatActions.Add(new MusicBeatSystem.BeatAction(Tick, 0));
        GenerateLine(100);
        GenerateLineGraphics();
        BarRef.OnGameOver.AddListener(OnGameOver);
        
        var node = PatronDatas.First;
        for (int i = 0; node.Next != null; i++){
            if(node.Value.lineSpotChange != null)
            node.Value.lineSpotChange(i);
            node = node.Next;
        }
    }

    public void Tick(){
         OnTick?.Invoke();
    }

    [Button]
    public void Admit() {
        PatronData p = PatronDatas.First.Value;
        if (p == null) return;

        PatronDatas.RemoveFirst();
        p.patron.CharacterAnimator.MoveToEnter();
        OnTick -= p.patron.Tick;
        p.patron.CharacterAnimator.OnArrival -= Interact;
        BarRef.Enter(p);
        p.patron.CharacterAnimator.OnArrival += () => { p.patron.SetSortingLayer("CharacterClub", 0); };

        OnLeaveLine.Invoke(p);
        PatronDatas.RemoveFirst();
        AddPatronData();
        CreatePatron();

        var node = PatronDatas.First;
        
        node.Value.patron.CharacterAnimator.OnArrival += Interact;

        for (int i = 0; node.Next != null; i++){
            if(node.Value.lineSpotChange != null)
                node.Value.lineSpotChange(i);
            node = node.Next;
        }
    }
    
    [Button]
    public void Reject() {
        PatronData p = PatronDatas.First.Value;
        if (p == null) return;
        p.patron.CharacterAnimator.MoveToLeave();
        p = PatronDatas.First.Value;
        p.patron.SetSortingLayer("CharacterLeave", 0);
        PatronDatas.RemoveFirst();
        OnTick -= p.patron.Tick;
        p.patron.CharacterAnimator.OnArrival -= Interact;
        OnLeaveLine.Invoke(p);

        if (Random.Range(0f, 1f) > 0.25f) { // Will they try going back into line?
            PatronData.RandomizePatronData(ref p);
        }
        
        PatronDatas.AddLast(p);
        CreatePatron();

        var node = PatronDatas.First;
        
        node.Value.patron.CharacterAnimator.OnArrival += Interact;
        for (int i = 0; node.Next != null; i++){
            if(node.Value.lineSpotChange != null)
                node.Value.lineSpotChange(i);
            node = node.Next;
        }
    }

    [Button]
    public void Interact() {
        PatronData p = PatronDatas.First.Value;
        if (p == null) return;
        OnInteract.Invoke(p);
    }

    public void GenerateLine(int size, bool useBaked = true, float probability = 0.1f) {
        for (int i = 0; i < size; i++) {
            if (useBaked && Random.Range(0f, 1f) > probability && BakedPatrons.Count > 0){
                int index = Random.Range(0, BakedPatrons.Count);
                PatronDatas.AddLast(BakedPatrons[index]);
                BakedPatrons.RemoveAt(index);
                continue;
            }

            PatronDatas.AddLast(PatronData.GeneratePatronData());
        }
    }
    
    public void GenerateLineGraphics() {
        for(int i = 0; i < 10; i++)
            CreatePatron();
    }

    public void OnGameOver(){
        foreach (var patron in PatronDatas){
            if(patron.patron != null)
                patron.patron.CharacterAnimator.MoveToLeave();
        }
    }

    [Button]
    public void CreatePatron() {
        for (var node = PatronDatas.First; node != null; node = node.Next){
            if (node.Value.patron != null)
                continue;
            node.Value.CreatePatron();
            node.Value.patron.OnLeave += Reject; //If they decide to leave, just reject yourself
            OnTick += node.Value.patron.Tick;
            break;
        }
    }
    
    public void AddPatronData(){
        PatronDatas.AddLast(PatronData.GeneratePatronData());
    }
}