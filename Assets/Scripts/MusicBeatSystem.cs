using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using FMODUnity;
using UnityEngine;

public class MusicBeatSystem : MonoBehaviour
{
    public List<BeatAction> OnBeatActions = new();
    public Bar bar;

    public float globalOffset = 0;

    public StudioEventEmitter music;
    public StudioEventEmitter crowd;

    private bool musicGoing = false;

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
        music = GetComponent<StudioEventEmitter>();
    }

    IEnumerator Start()
    {
        beatInterval = 60f / 118f;
        yield return new WaitForSeconds(1f);
        StartMusic();
    }

    [Button(Mode = ButtonMode.EnabledInPlayMode)]
    public void StartMusic()
    {
        musicGoing = true;
        startTime = Time.time;
        music.Play();
        crowd.Play();
    }

    public static float Remap(float iMin, float iMax, float oMin, float oMax, float v)
    {
        float t = Mathf.InverseLerp(iMin, iMax, v);
        return Mathf.Lerp(oMin, oMax, t);
    }

    private void Update()
    {
        float e = Remap(bar.MinEnergy, bar.MaxEnergy, 0f, 2f, bar.Energy);
        float h = Remap(0, bar.MaxHeat, 0f, 2f, bar.EnergyHeat);
        music.SetParameter("Energy", e);
        
        float c = Remap(0, bar.MaxPatrons, 0f, 1f, bar.Patrons.Count);
        music.SetParameter("Capacity", c);
        
        foreach (BeatAction onBeatAction in OnBeatActions)
        {
            if (Time.time - startTime >= onBeatAction.nextTime)
            {
                if (musicGoing)
                    onBeatAction.onBeat?.Invoke();
                onBeatAction.currentBeat++;
                onBeatAction.nextTime =
                    (onBeatAction.currentBeat * beatInterval) + onBeatAction.timeOffset + globalOffset;
            }
        }
    }

    [Button]
    public void KillMusic() => StartCoroutine(CR_KillMusic());

    IEnumerator CR_KillMusic()
    {
        musicGoing = false;
        float t = 0;
        while (t < 2)
        {
            t += Time.deltaTime * 0.2f;
            music.SetParameter("Stop The Party", t);
            yield return null;
        }

        music.SetParameter("Stop The Party", 2f);
    }
}