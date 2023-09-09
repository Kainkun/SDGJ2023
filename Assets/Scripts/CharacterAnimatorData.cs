using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAnimatorData", menuName = "ScriptableObjects/CharacterAnimatorData")]
public class CharacterAnimatorData : ScriptableObject
{
    public AnimationCurve stepCurve;
    public float smoothTime;
    public float maxSpeed;
}