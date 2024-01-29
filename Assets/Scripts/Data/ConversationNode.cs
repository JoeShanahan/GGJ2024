using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConversationNode : ScriptableObject
{
    public List<ConversationLine> DialogueLines;
    public List<ConversationChoice> Choices;
    public Sprite InitialLeftPersonIcon;
    public Sprite InitialRightPersonIcon;
    public CharacterData InitialLeftPerson;
    public CharacterData InitialRightPerson;
    public bool DoesAdvanceProgression;
}

[System.Serializable]
public class ConversationLine
{
    public string LineText;
    public string PersonName;
    public Sprite PersonIcon;
    public AudioClip TalkSound;
    public CharacterData Person;
    public ItemData ClueMentioned;
    public bool isPersonOnLeftTalking;
}

[System.Serializable]
public class ConversationChoice
{
    public string ChoiceText;
    public ConversationNode NextNode;

    [Header("Required to see choice")]
    public ItemData[] RequiredItemList;
    public bool needAll;

    [Header("Only for deductions")]
    public int EndingPoints;
    public EndingType endingType;
}

public enum EndingType
{
    None, Bad, Silly, Good, SelfIncriminate
}
