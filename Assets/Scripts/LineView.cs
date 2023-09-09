using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class LineView : MonoBehaviour
{
    private static CharacterAnimatorData[] patronAnimatorDatas;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        patronAnimatorDatas = Resources.LoadAll<CharacterAnimatorData>("StepData");
    }

    [Button]
    private CharacterAnimator CreatePatron()
    {
        GameObject patron = new GameObject("Patron");
        GameObject sprite = PatronView.CreateRandomPatron();
        sprite.transform.parent = patron.transform;
        CharacterAnimator characterAnimator = patron.AddComponent<CharacterAnimator>();
        characterAnimator.data = patronAnimatorDatas[Random.Range(0, patronAnimatorDatas.Length)];
        characterAnimator.MoveToWaitPosition();
        return characterAnimator;
    }
}
