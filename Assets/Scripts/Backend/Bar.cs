using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bar : MonoBehaviour
{
    public int Energy, MaxEnergy, MinEnergy;
    public int MaxPeople, MinPeople;
    public List<PatronData> Patrons = new List<PatronData>();

    //Heat like the Feds
    public int EnergyHeat; 
    public int PatronHeat;
    public int MaxHeat = 15;
    
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
        CheckEnergyHeat();
        CheckPatronHeat();
        OnTick?.Invoke();
    }

    private void CheckEnergyHeat() {
        if (Energy > MaxEnergy || Energy < MinEnergy)
            EnergyHeat += 1;
        else
            EnergyHeat -= Math.Max(0, EnergyHeat - 1);
    }

    private void CheckPatronHeat() {
        if (Energy > MaxEnergy || Energy < MinEnergy)
            EnergyHeat += 1;
        else
            EnergyHeat -= Math.Max(0, EnergyHeat - 1);

        if (EnergyHeat >= MaxHeat) {
            
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
        OnTick.AddListener(p.patron.Tick);

        float chaosThresh = 1 - ((float)(Energy + p.energy) / (float)MaxEnergy);
        if (p.chaos > chaosThresh) {
            ChaoticEvent();
        }

        Energy += p.energy;
    }

    public void Exit(PatronData p) {
        Patrons.Remove(p);
        p.barRef = null;
        OnTick.RemoveListener(p.patron.Tick);
        
        float chaosThresh = 1 - ((float)(Energy + p.energy) / (float)MaxEnergy);
        if (p.chaos > chaosThresh) {
            ChaoticEvent();
        }
        
        Energy -= p.energy;
        
        Destroy(p);
    }
}
