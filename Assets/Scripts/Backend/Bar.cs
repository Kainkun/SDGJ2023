using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bar : MonoBehaviour
{
    public int Energy, MaxEnergy, MinEnergy;
    public int MaxPeople, MinPeople;
    public List<Patron> Patrons = new List<Patron>();

    //Heat like the Feds
    public int EnergyHeat; 
    public int PatronHeat;
    
    //begin function prototypes
    public UnityEvent OnGameOver;
    public UnityEvent OnTick;
    public UnityEvent OnChaoticEvent;

    //Tick is a unit of time
    private void Tick() {
        //TODO :: What needs to be done in tick
        CheckEnergyHeat();
        CheckPatronHeat();
        OnTick.Invoke();
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
    }

    private void ChaoticEvent() {
        OnChaoticEvent.Invoke();
    }

    private void GameOver(){
        OnGameOver.Invoke();
    }

    public void Enter(Patron p) {
        Patrons.Add(p);
        p.BarRef = this;
        OnTick.AddListener(p.Tick);

        float chaosThresh = 1 - ((float)(Energy + p.Energy) / (float)MaxEnergy);
        if (p.Chaos > chaosThresh) {
            ChaoticEvent();
        }

        Energy += p.Energy;
    }

    public void Exit(Patron p) {
        Patrons.Remove(p);
        p.BarRef = null;
        OnTick.RemoveListener(p.Tick);
        
        float chaosThresh = 1 - ((float)(Energy + p.Energy) / (float)MaxEnergy);
        if (p.Chaos > chaosThresh) {
            ChaoticEvent();
        }
        
        Energy -= p.Energy;
    }
}
