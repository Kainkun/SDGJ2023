using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnimationUI : MonoBehaviour
{
    public AnimationCurve xScale;
    public AnimationCurve yScale;


    IEnumerator Start()
    {
        while (true)
        {
            Bounce();
            yield return new WaitForSeconds(0.5f);
        }
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