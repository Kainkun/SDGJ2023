using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private float distanceToWaitPosition = 2;
    private Vector2 waitPosition = new Vector2(-1.5f, -0.8f);
    private float velocity;

    private float stepDistance;
    private float stepRemainder;

    public CharacterAnimatorData data;

    public Action onWaitPosition;
    public Action onDisappear;

    void Start()
    {
        stepDistance = data.stepCurve.keys[data.stepCurve.length - 1].time;
        stepRemainder = distanceToWaitPosition % stepDistance;

        StartCoroutine(MoveToWaitPosition());
    }

    IEnumerator MoveToWaitPosition()
    {
        Vector2 startPosition = waitPosition + Vector2.left * distanceToWaitPosition;
        transform.position = startPosition;

        float t = 0;
        while (t < distanceToWaitPosition)
        {
            t = Mathf.SmoothDamp(t, distanceToWaitPosition, ref velocity, data.smoothTime, data.maxSpeed);
            transform.position = startPosition + new Vector2(t, data.stepCurve.Evaluate(t - stepRemainder));
            yield return null;
        }

        transform.position = waitPosition;

        onWaitPosition?.Invoke();
    }

    IEnumerator MoveToLeave()
    {
        Vector2 startPosition = waitPosition;
        transform.position = startPosition;

        float t = 0;
        while (t < distanceToWaitPosition)
        {
            t = Mathf.SmoothDamp(t, distanceToWaitPosition, ref velocity, data.smoothTime, data.maxSpeed);
            transform.position = startPosition + new Vector2(-t, data.stepCurve.Evaluate(t));
            yield return null;
        }
    }
}