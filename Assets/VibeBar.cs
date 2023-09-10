using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibeBar : MonoBehaviour
{
    public Bar bar;
    public Image slider;
    public Image happy;
    public Image sad;
    public AnimationCurve energyCurve;
    public AnimationCurve capCurve;

    private void Update()
    {
        float energy = MusicBeatSystem.Remap(bar.MinEnergy, bar.MaxEnergy, 0f, 1f, bar.Energy);
        float capacity = MusicBeatSystem.Remap(0, bar.MaxPatrons, 0f, 1f, bar.Patrons.Count);
        float vibe = energyCurve.Evaluate(energy) * capCurve.Evaluate(capacity);
        slider.fillAmount = vibe;
        if(vibe > 0.5f)
        {
            happy.enabled = true;
            sad.enabled = false;
        }
        else
        {
            happy.enabled = false;
            sad.enabled = true;
        }
    }
}
