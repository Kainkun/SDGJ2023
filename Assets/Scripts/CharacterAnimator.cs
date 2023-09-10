using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private float distanceToWaitPosition = 5;
    private float distanceToEnterPosition = 4.5f;
    private Vector2 waitPosition = new Vector2(-0.4f, -2.2f);
    private float velocity;

    private float stepDistance;
    private float stepRemainder;

    public CharacterAnimatorData data;

    public Action onWaitPosition;
    public Action onDisappear;

#if UNITY_EDITOR
    [ReadOnly]
#endif
    public State state;

    public enum State
    {
        BeforeAppear,
        MovingToWaitPosition,
        Waiting,
        MovingToLeave,
        MovingToEnter,
        Disappeared
    }

    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    public void MoveToWaitPosition() => StartCoroutine(CR_MoveToWaitPosition());

    IEnumerator CR_MoveToWaitPosition()
    {
        state = State.BeforeAppear;
        stepDistance = data.stepCurve.keys[data.stepCurve.length - 1].time;
        stepRemainder = distanceToWaitPosition % stepDistance;

        if (state is not State.BeforeAppear)
        {
            Debug.LogWarning("CharacterAnimator is not in a valid state to move to wait position.");
            yield break;
        }

        state = State.MovingToWaitPosition;

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
    public void MoveToLeave() => StartCoroutine(CR_MoveToLeave());

    IEnumerator CR_MoveToLeave()
    {
        if (state is not (State.MovingToWaitPosition or State.Waiting))
        {
            Debug.LogWarning("CharacterAnimator is not in a valid state to move to leave position.");
            yield break;
        }

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

        state = State.Disappeared;
        onDisappear?.Invoke();

        Destroy(gameObject);
    }

    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    public void MoveToEnter() => StartCoroutine(CR_MoveToEnter());

    IEnumerator CR_MoveToEnter()
    {
        if (state is not (State.MovingToWaitPosition or State.Waiting))
        {
            Debug.LogWarning("CharacterAnimator is not in a valid state to move to enter position.");
            yield break;
        }

        yield return new WaitUntil(() => state == State.Waiting);

        state = State.MovingToEnter;

        Vector2 startPosition = waitPosition;
        transform.position = startPosition;

        float t = 0;
        while (t < distanceToEnterPosition - 0.001f)
        {
            t = Mathf.SmoothDamp(t, distanceToEnterPosition, ref velocity, data.smoothTime, data.maxSpeed);
            transform.position = startPosition + new Vector2(t, data.stepCurve.Evaluate(t));
            yield return null;
        }

        state = State.Disappeared;
        onDisappear?.Invoke();

        Destroy(gameObject);
    }
}