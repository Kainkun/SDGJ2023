using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron
{
    public int Patience, Chaos;
    public int Energy, Duration;
    public Bar BarRef;
    public Patron();

    private void Tick()
    {
        CalcChaosEvent();
        CalcDuration();
    }
    private void CalcDuration()
    {
        some math bullshit
    }

    private void CalcChaosEvent()
    {
        some math bullshit
    }
}
