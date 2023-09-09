using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class DiscoLight : MonoBehaviour
{
    public float speed = 1f;
    public float range = 1f;
    private float seed;
    private Vector2 startPosition;

    Light2D light2D;
    [HideInInspector] public float startingIntensity;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        startingIntensity = light2D.intensity;
        startPosition = transform.localPosition;
        seed = Random.Range(0f, 1000f);
    }

    public void MultiplyIntensity(float multiplier)
    {
        light2D.intensity = startingIntensity * multiplier;
    }

    void Update()
    {
        Vector2 position;
        position.x = Mathf.PerlinNoise(Time.time * speed + seed, 0) * 2 - 1;
        position.y = Mathf.PerlinNoise(0, Time.time * speed + seed) * 2 - 1;
        transform.localPosition = startPosition + position * range;
    }
}