using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Patron : MonoBehaviour
{
    public int Patience;
    public float Chaos;
    public int Energy, Duration;
    public Bar BarRef;

    public UnityEvent ChaosEvent;

    public Patron(int patience, float chaos, int energy, int duration) {
        Patience = patience;
        Chaos = chaos;
        Energy = energy;
        Duration = duration;
    }

    public Patron() : this(Random.Range(0, 100), 
                            Random.Range(0, 100),
                            Random.Range(0, 100),
                            Random.Range(0, 100))
    { }

    public void Tick() {
        Duration -= 1;
    }
}
