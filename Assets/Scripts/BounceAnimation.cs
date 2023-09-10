using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    public AnimationCurve xScale;
    public AnimationCurve yScale;


    private void Start()
    {
        MusicBeatSystem.Instance.OnBeatActions.Add(new MusicBeatSystem.BeatAction(Bounce, -0.2f));
    }

    private void Bounce() => StartCoroutine(CR_Bounce());

    IEnumerator CR_Bounce()
    {
        float t = 0;

        float time = Mathf.Max(xScale.keys[xScale.length - 1].time, yScale.keys[yScale.length - 1].time);

        while (t < time)
        {
            t += Time.deltaTime;
            transform.localScale = new Vector3(xScale.Evaluate(t), yScale.Evaluate(t), 1);
            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}