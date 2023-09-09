using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bar
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

    private void GameOver(){
        OnGameOver.Invoke();
    }

    public void Enter(Patron p) {
        Patrons.Add(p);
        p.BarRef = this;
        OnTick.AddListener(p.Tick);
        Energy += p.Energy;
    }

    public void Exit(Patron p) {
        Patrons.Remove(p);
        p.BarRef = null;
        OnTick.RemoveListener(p.Tick);
        Energy -= p.Energy;
    }
}
