using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.Assertions;

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

    [ReadOnly] public State state;

    public enum State
    {
        BeforeAppear,
        MovingToWaitPosition,
        Waiting,
        MovingToLeave,
        MovingToEnter,
        Disappeared
    }

    void Awake()
    {
        sprite.SetActive(false);
        state = State.BeforeAppear;
        stepDistance = data.stepCurve.keys[data.stepCurve.length - 1].time;
        stepRemainder = distanceToWaitPosition % stepDistance;
    }

    private void Start()
    {
        MoveToWaitPosition();
    }

    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    private void MoveToWaitPosition() => StartCoroutine(E_MoveToWaitPosition());

    IEnumerator E_MoveToWaitPosition()
    {
        Assert.IsTrue(state == State.BeforeAppear);

        state = State.MovingToWaitPosition;

        sprite.SetActive(true);

        Vector2 startPosition = waitPosition + Vector2.left * distanceToWaitPosition;
        transform.position = startPosition;

        float t = 0;
        while (t < distanceToWaitPosition - 0.001f)
        {
            t = Mathf.SmoothDamp(t, distanceToWaitPosition, ref velocity, data.smoothTime, data.maxSpeed);
            transform.position = startPosition + new Vector2(t, data.stepCurve.Evaluate(t - stepRemainder));
            yield return null;
        }

        transform.position = waitPosition;
        state = State.Waiting;
        onWaitPosition?.Invoke();
    }

    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    private void MoveToLeave() => StartCoroutine(E_MoveToLeave());

    IEnumerator E_MoveToLeave()
    {
        Assert.IsTrue(state is State.MovingToWaitPosition or State.Waiting);

        yield return new WaitUntil(() => state == State.Waiting);

        state = State.MovingToLeave;

        Vector2 startPosition = waitPosition;
        transform.position = startPosition;

        float t = 0;
        while (t < distanceToWaitPosition - 0.001f)
        {
            t = Mathf.SmoothDamp(t, distanceToWaitPosition, ref velocity, data.smoothTime, data.maxSpeed);
            transform.position = startPosition + new Vector2(-t, data.stepCurve.Evaluate(t));
            yield return null;
        }

        sprite.SetActive(false);
        state = State.Disappeared;
        onDisappear?.Invoke();
    }
}