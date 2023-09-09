using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using FMODUnity;
using UnityEngine;

public class MusicBeatSystem : MonoBehaviour
{
    public List<BeatAction> OnBeatActions = new();

    public class BeatAction
    {
        public Action onBeat;
        public float timeOffset;
        public int currentBeat;
        public float nextTime;

        public BeatAction(Action onBeat, float timeOffset)
        {
            this.onBeat = onBeat;
            this.timeOffset = timeOffset;
            currentBeat = 0;
            nextTime = 0;
        }
    }

    private float startTime = float.PositiveInfinity;
    private float beatInterval;

    public static MusicBeatSystem Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        Instance = null;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        beatInterval = 60f / 112f;
    }
    
    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    public void StartMusic()
    {
        startTime = Time.time;
        GetComponent<StudioEventEmitter>().Play();
    }

    private void Update()
    {
        foreach (BeatAction onBeatAction in OnBeatActions)
        {
            if (Time.time - startTime >= onBeatAction.nextTime)
            {
                onBeatAction.onBeat?.Invoke();
                onBeatAction.currentBeat++;
                onBeatAction.nextTime = (onBeatAction.currentBeat * beatInterval) + onBeatAction.timeOffset;
            }
        }
    }
}