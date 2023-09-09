using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float startZoom;
    public float endZoom;
    
    public AnimationCurve moveCurve;
    public AnimationCurve zoomCurve;

    [Button]
    private void SetStartPos()
    {
        startPos = transform.position;
        startZoom = Camera.main.orthographicSize;
    }

    [Button]
    private void SetEndPos()
    {
        endPos = transform.position;
        endZoom = Camera.main.orthographicSize;
    }

    [Button]
    private void GoToStartPos()
    {
        transform.position = startPos;
        Camera.main.orthographicSize = startZoom;
    }

    [Button]
    private void GoToEndPos()
    {
        transform.position = endPos;
        Camera.main.orthographicSize = endZoom;
    }

    private Vector3 velocity;
    private float zoomVelocity;

    private IEnumerator Start()
    {
        GoToStartPos();

        float maxTime = Mathf.Max(moveCurve.keys[moveCurve.length - 1].time, zoomCurve.keys[zoomCurve.length - 1].time);
        float t = 0;
        while (t < maxTime)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, moveCurve.Evaluate(t));
            Camera.main.orthographicSize = Mathf.Lerp(startZoom, endZoom, zoomCurve.Evaluate(t));
            yield return null;
        }
    }
}