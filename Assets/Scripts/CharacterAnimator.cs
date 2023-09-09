using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public GameObject sprite;

    private float distanceToWaitPosition = 2;
    private Vector2 waitPosition = new Vector2(-1.5f, 0f);
    private float velocity;

    private float stepDistance;
    private float stepRemainder;

    public CharacterAnimatorData data;

    public Action onWaitPosition;
    public Action onDisappear;

    void Start()
    {
        sprite.SetActive(false);

        stepDistance = data.stepCurve.keys[data.stepCurve.length - 1].time;
        stepRemainder = distanceToWaitPosition % stepDistance;
    }

    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    public void MoveToWaitPosition() => StartCoroutine(E_MoveToWaitPosition());

    IEnumerator E_MoveToWaitPosition()
    {
        sprite.SetActive(true);

        Vector2 startPosition = waitPosition + Vector2.left * distanceToWaitPosition;
        transform.position = startPosition;

        float t = 0;
        while (t < distanceToWaitPosition - 0.001f)
        {
            print(t);
            t = Mathf.SmoothDamp(t, distanceToWaitPosition, ref velocity, data.smoothTime, data.maxSpeed);
            transform.position = startPosition + new Vector2(t, data.stepCurve.Evaluate(t - stepRemainder));
            yield return null;
        }

        transform.position = waitPosition;
        onWaitPosition?.Invoke();
    }

    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    public void MoveToLeave() => StartCoroutine(E_MoveToLeave());

    IEnumerator E_MoveToLeave()
    {
        Vector2 startPosition = waitPosition;
        transform.position = startPosition;

        float t = 0;
        while (t < distanceToWaitPosition - 0.001f)
        {
            print(t);
            t = Mathf.SmoothDamp(t, distanceToWaitPosition, ref velocity, data.smoothTime, data.maxSpeed);
            transform.position = startPosition + new Vector2(-t, data.stepCurve.Evaluate(t));
            yield return null;
        }

        sprite.SetActive(false);
        onDisappear?.Invoke();
    }
}