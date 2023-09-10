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
    
    //begin function prototypes
    public UnityEvent OnGameOver;
    public UnityEvent OnTick;
    public UnityEvent OnChaoticEvent;

    private void Start()
    {
        StartCoroutine(Ticks());
    }

    //Tick is a unit of time
    private IEnumerator Ticks() {
        while (true)
        {
            yield return new WaitForSeconds(.25f);
            Tick();
        }
    }

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

    public void Enter(PatronData p) {
        Patrons.Add(p);
        p.BarRef = this;
        OnTick.AddListener(Tick);

        float chaosThresh = 1 - ((float)(Energy + p.Energy) / (float)MaxEnergy);
        if (p.Chaos > chaosThresh) {
            ChaoticEvent();
        }

        Energy += p.Energy;
    }

    public void Exit(PatronData p) {
        Patrons.Remove(p);
        p.BarRef = null;
        OnTick.RemoveListener(Tick);
        
        float chaosThresh = 1 - ((float)(Energy + p.Energy) / (float)MaxEnergy);
        if (p.Chaos > chaosThresh) {
            ChaoticEvent();
        }
        
        Energy -= p.Energy;
        
        Destroy(p);
    }
}
