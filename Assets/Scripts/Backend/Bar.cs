using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bar : MonoBehaviour
{
    public int Energy, MaxEnergy = 100, MinEnergy = 10;
    public int MaxPatrons, MinPatrons;
    public List<PatronData> Patrons = new List<PatronData>();

    //Heat like the Feds
    public int EnergyHeat; 
    public int PatronHeat;
    public int MaxHeat = 30;

    public int preRoundCountDown = 100;
    
    //begin function prototypes
    public UnityEvent OnGameOver;
    public UnityEvent OnTick;
    public UnityEvent OnChaoticEvent;

    private void Start()
    {
        MusicBeatSystem.Instance.OnBeatActions.Add(new MusicBeatSystem.BeatAction(Tick, 0));
    }

    private void Tick() {
        //TODO :: What needs to be done in tick
        preRoundCountDown--;
        CheckEnergyHeat();
        CheckPatronHeat();
        if (preRoundCountDown > 0)
            Energy = MaxEnergy / 2;
        if(preRoundCountDown == 0)
            PreroundEnd(); 
        OnTick?.Invoke();
    }

    private void CheckEnergyHeat(){
        if (preRoundCountDown > 0)
            return;
        
        if (Energy > MaxEnergy || Energy < MinEnergy)
            EnergyHeat += 1;
        else
            EnergyHeat -= Math.Max(0, EnergyHeat - 1);
        
        if (EnergyHeat >= MaxHeat) {
            GameOver();
        }
    }

    private void CheckPatronHeat() {
        if (preRoundCountDown > 0)
            return;

        if (Patrons.Count > MaxPatrons || Patrons.Count < MinPatrons)
            PatronHeat += 1;
        else
            PatronHeat -= Math.Max(0, EnergyHeat - 1);

        if (PatronHeat >= MaxHeat) {
            GameOver();
        }
    }

    private void ChaoticEvent() {
        OnChaoticEvent.Invoke();
    }

    private void GameOver(){
        OnGameOver.Invoke();
    }

    public void Enter(PatronData p) {
        Patrons.Add(p);
        p.barRef = this;
        OnTick.AddListener(p.Tick);

        float chaosThresh = 1 - ((float)(Energy + p.energy) / (float)MaxEnergy);
        if (p.chaos > chaosThresh) {
            ChaoticEvent();
        }

        Energy += p.energy;
    }

    public void Exit(PatronData p) {
        Patrons.Remove(p);
        p.barRef = null;
        OnTick.RemoveListener(p.Tick);
        
        float chaosThresh = 1 - ((float)(Energy + p.energy) / (float)MaxEnergy);
        if (p.chaos > chaosThresh) {
            ChaoticEvent();
        }
        
        Energy -= p.energy;
        
        Destroy(p);
    }

    void PreroundEnd() {
        Energy = 0;
        foreach (var patron in Patrons) {
            Energy += patron.energy;
        }
    }
}
