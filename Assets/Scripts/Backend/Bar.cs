using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bar
{
    public int Energy, MaxEnergy, MinEnergy;
    public int MaxPeople, MinPeople;
    public int Guess;
    public Patron Patrons[];
    
    /* In Case we need this later
    static private Bar _instance;
    static public Bar Instance
    {
        get
        {
            if (_instance == null)
                _instance = this;
            return _instance;
        }
    }
    */
    
    
    //begin function prototypes
    public UnityEvent GameOver;
    public UnityEvent OnTick;
    private void Tick();
    public void Enter();
    public void Exit();
}
