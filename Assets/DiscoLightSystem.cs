using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DiscoLightSystem : MonoBehaviour
{
    private DiscoLight[] lights;

    public AnimationCurve pulse;

    void Start()
    {
        lights = GetComponentsInChildren<DiscoLight>();
    }

    private void Update()
    {
        foreach (var light in lights)
        {
            light.MultiplyIntensity(pulse.Evaluate(Time.time));
        }
    }
}