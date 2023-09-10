using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimator : MonoBehaviour
{
    public Transform lineFront;
    public Transform spawnPosition;
    public Transform enterClubPosition;

    public CharacterAnimatorData data;
    
    public UnityEvent OnArrival;
    public Action onDisappear;

    public Vector3 initialScale;

    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    public void MoveToWaitPosition() => SetQueue(MoveTo(this.transform.position, new Vector2(lineFront.position.x, lineFront.position.y)));

    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    public void MoveToLeave() => SetQueue(MoveTo(this.transform.position, new Vector2(spawnPosition.position.x, spawnPosition.position.y)));
    
    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    public void MoveToEnter() => SetQueue(MoveTo(this.transform.position, new Vector2(enterClubPosition.position.x, enterClubPosition.position.y)));
    
    void Awake(){
        lineFront = GameObject.Find("LineFront").transform;
        spawnPosition = GameObject.Find("Spawn").transform;
        enterClubPosition = GameObject.Find("EnterClubPosition").transform;
        this.transform.position = spawnPosition.position;
        initialScale = this.transform.localScale;
    }
    

    public float stepDistance, stepsRemaining, t, loop;

    private Coroutine queue;
    private Coroutine current;
    public void SetQueue(IEnumerator c){
        if(queue != null) StopCoroutine(queue);
        queue = StartCoroutine(QueueWait(c));
    }

    IEnumerator QueueWait(IEnumerator c){
        yield return new WaitUntil(() => current == null);
        current = StartCoroutine(c);
        queue = null;
    }

    IEnumerator MoveTo(Vector2 inital, Vector2 goal){
        queue = null;
        stepDistance = data.stepCurve.keys[data.stepCurve.length - 1].time;
        stepsRemaining = (this.transform.position.x - goal.x) / stepDistance;

        Vector3 scale = initialScale;
        scale.x *= Mathf.Sign(-stepsRemaining);
        this.transform.localScale = scale;
        
        float y = this.transform.position.y;

        loop = Mathf.Abs(stepsRemaining);
        for (float velocity=0; loop >= 0.0001f; loop = Mathf.Abs(stepsRemaining)){
            stepsRemaining = (this.transform.position.x - goal.x) / stepDistance;
            t = Mathf.SmoothDamp(this.transform.position.x, goal.x, ref velocity, data.smoothTime, data.maxSpeed);
            this.transform.position = new Vector3(t, y + data.stepCurve.Evaluate(Mathf.Abs(t)));
            if (queue != null){
                if(data.stepCurve.Evaluate(Mathf.Abs(t)) <= 0.01f){
                    this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
                    current = null;
                    yield break; //Exit at nearest opportunity
                }
            }
            yield return new WaitForEndOfFrame();
        }

        this.transform.position = new Vector3(goal.x, y, this.transform.position.z);
        OnArrival?.Invoke();
        current = null;
    }
}