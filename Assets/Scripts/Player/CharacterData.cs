using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum BodyPart
{
    None = 0,
    RightHand = 1,
    LeftHand = 2,
    RightFoot = 4,
    LeftFoot = 8,
    Head = 16
}

[Serializable]
public class AttackData
{
    public AnimationClip Animation;

    // TODO implement this, with button memory too (store a button press and apply it)
    [Range(0, 1), Tooltip("How much of the clip must play before moving on")]
    public float MinimumTime = 0.8f;

    // TODO implement this
    [Range(0, 3), Tooltip("What percentage through the clip must the player hit attack BEFORE to continue the combo?")]
    public float ComboTime = 1f;

    [Range(0, 5), Tooltip("How much damage should this attack do?")]
    public float DamageMultiplier = 1f;

    [Tooltip("Which body parts should do damage during this attack?")]
    public BodyPart BodyParts;

    [Tooltip("How large should each hitbox be during the attack?")]
    public AnimationCurve HitSize;

    [Tooltip("How far forward should the player move (if at all) when attacking?")]
    public AnimationCurve ForwardMovement;

    [Tooltip("Is this attack a combo finisher?")]
    public float LaunchForce;

    public float AttackLength => Animation.length;
}

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{   
    public string CharacterName;
    public Sprite Portrait;
    public AttackData[] Attacks;
    public Color BgColorA;
    public Color BgColorB;
    public GameObject Prefab;
}
