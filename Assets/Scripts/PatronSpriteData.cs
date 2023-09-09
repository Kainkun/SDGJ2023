using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSpriteData", menuName = "ScriptableObjects/CharacterSpriteData")]
public class PatronSpriteData : ScriptableObject
{
    public GameObject psb
    {
        get { return Resources.Load<GameObject>("SpritePsb/" + name); }
    }

    public Gradient b1;
    public Gradient b2;
    public Gradient b3;
}