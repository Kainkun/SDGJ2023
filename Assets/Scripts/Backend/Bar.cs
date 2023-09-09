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
        CheckEnergy();
        CheckPatronCount();
        OnTick.Invoke();
    }

    private void CheckEnergy() {
        
    }

    private void CheckPatronCount() {
        
    }

    private void GameOver(){
        OnGameOver.Invoke();
    }

    public void Enter(Patron p) {
        Patrons.Add(p);
        Energy += p.Energy;
    }

    public void Exit(Patron p) {
        Patrons.Remove(p);
        Energy -= p.Energy;
    }
}
