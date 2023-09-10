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

    public void Start() {
        MusicBeatSystem.Instance.OnBeatActions.Add(new MusicBeatSystem.BeatAction(Tick, 0));
        GenerateLine(100);
        CreatePatron();
    }

    public void Tick(){
         OnTick?.Invoke();
    }

    [Button]
    public void Admit() {
        PatronData p = PatronDatas.First.Value;
        if (p == null) return;
        if (p.patron.CharacterAnimator.state != CharacterAnimator.State.Waiting) {
            return;
        }

        PatronDatas.RemoveFirst();
        p.patron.CharacterAnimator.MoveToEnter();
        OnTick -= p.patron.Tick;
        BarRef.Enter(p);
        PatronDatas.RemoveFirst();
        AddPatronData();
        if (PatronDatas.First.Value == null) return;
        if(PatronDatas.First.Value.patron == null)
            CreatePatron();
        PatronDatas.First.Value.patron.CharacterAnimator.MoveToWaitPosition();
    }
    
    [Button]
    public void Reject() {
        PatronData p = PatronDatas.First.Value;
        if (p == null) return;
        if (p.patron.CharacterAnimator.state != CharacterAnimator.State.Waiting) {
            return;
        }
        
        p.patron.CharacterAnimator.MoveToLeave();
        p = PatronDatas.First.Value;
        PatronDatas.RemoveFirst();
        OnTick -= p.patron.Tick;

        if (Random.Range(0f, 1f) > 0.25f) { // Will they try going back into line?
            PatronData.RandomizePatronData(ref p);
        }
        
        PatronDatas.AddLast(p);
        if (PatronDatas.First.Value == null) return;
        if(PatronDatas.First.Value.patron == null)
            CreatePatron();
        
        PatronDatas.First.Value.patron.CharacterAnimator.MoveToWaitPosition();
    }

    [Button]
    public void Interact() {
        PatronData p = PatronDatas.First.Value;
        if (p == null) return;
        if (p.patron.CharacterAnimator.state != CharacterAnimator.State.Waiting) {
            return;
        }
        OnInteract.Invoke(p);
    }

    public void GenerateLine(int size, bool useBaked = true, float probability = 0.1f) {
        for (int i = 0; i < size; i++) {
            if (useBaked && Random.Range(0f, 1f) > probability && BakedPatrons.Count > 0) {
                PatronDatas.AddLast(BakedPatrons[Random.Range(0, BakedPatrons.Count)]);
                continue;
            }

            PatronDatas.AddLast(PatronData.GeneratePatronData());
        }
    }

    [Button]
    public void CreatePatron() {
        if (PatronDatas.First.Value == null)
            return;
        PatronDatas.First.Value.CreatePatron();
        PatronDatas.First.Value.patron.OnLeave += Reject; //If they decide to leave, just reject yourself
        OnTick += PatronDatas.First.Value.patron.Tick;
        
    }
    
    public void AddPatronData(){
        PatronDatas.AddLast(PatronData.GeneratePatronData());
    }


}