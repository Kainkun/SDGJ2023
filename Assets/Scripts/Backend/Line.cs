using System;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using Random = UnityEngine.Random;

public class Line : MonoBehaviour
{
    private static CharacterAnimatorData[] patronAnimatorDatas;
    private static CharacterSpriteData[] patronSpriteDatas;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init() {
        patronAnimatorDatas = Resources.LoadAll<CharacterAnimatorData>("StepData");
        patronSpriteDatas = Resources.LoadAll<CharacterSpriteData>("SpriteData");
    }

    public LinkedList<PatronData> PatronDatas = new LinkedList<PatronData>();
    public List<PatronData> BakedPatrons = new List<PatronData>();
    public Bar BarRef;

    public void Start() {
        GenerateLine(100);
        CreatePatron();
    }

    public void Admit() {
        PatronData p = PatronDatas.First.Value;
        PatronDatas.RemoveFirst();
        if (p == null) return;
        if (p.patron.CharacterAnimator.state != CharacterAnimator.State.Waiting) {
            return;
        }

        p.patron.CharacterAnimator.MoveToEnter();
        BarRef.Enter(p);
        PatronDatas.RemoveFirst();
        AddPatronData();
        PatronDatas.First.Value.patron.CharacterAnimator.MoveToWaitPosition();
    }
    
    public void Reject() {
        PatronData p = PatronDatas.First.Value;
        if (p == null) return;
        if (p.patron.CharacterAnimator.state != CharacterAnimator.State.Waiting) {
            return;
        }
        
        p.patron.CharacterAnimator.MoveToLeave();
        p = PatronDatas.First.Value;
        PatronDatas.RemoveFirst();

        if (Random.Range(0, 1) > 0.25f) { // Will they try going back into line?
            PatronData.RandomizePatronData(ref p,
                patronSpriteDatas[Random.Range(0, patronSpriteDatas.Length)],
                patronAnimatorDatas[Random.Range(0, patronAnimatorDatas.Length)]);
        }
        
        PatronDatas.AddLast(p);
        if (PatronDatas.First.Value == null) return;
        if(PatronDatas.First.Value.patron == null)
            CreatePatron();
        
        PatronDatas.First.Value.patron.CharacterAnimator.MoveToWaitPosition();
    }

    public void Interact() {
        PatronData p = PatronDatas.First.Value;
        if (p == null) return;
        if (p.patron.CharacterAnimator.state != CharacterAnimator.State.Waiting) {
            return;
        }
    }

    public void GenerateLine(int size, bool useBaked = true, float probability = 0.1f) {
        for (int i = 0; i < size; i++) {
            if (useBaked && Random.Range(0, 1) > probability) {
                PatronDatas.AddLast(BakedPatrons[Random.Range(0, BakedPatrons.Count)]);
                continue;
            }

            PatronDatas.AddLast(PatronData.GeneratePatronData(
                patronSpriteDatas[Random.Range(0, patronSpriteDatas.Length)],
                patronAnimatorDatas[Random.Range(0, patronAnimatorDatas.Length)]));
        }
    }

    [Button]
    public void CreatePatron() {
        if (PatronDatas.First.Value == null)
            return;
        PatronDatas.First.Value.CreatePatron();
    }
    
    public void AddPatronData(){
        PatronDatas.AddLast(PatronData.GeneratePatronData(
            patronSpriteDatas[Random.Range(0, patronSpriteDatas.Length)],
            patronAnimatorDatas[Random.Range(0, patronAnimatorDatas.Length)]));
    }


}