using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public string CharacterName;
    public Sprite Sprite;
    public AudioClip TalkingSound;
}
