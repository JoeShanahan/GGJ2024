using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConversationNode : ScriptableObject
{
    public List<ConversationLine> DialogueLines;
    public List<ConversationChoice> Choices;
    public ItemData unlockedEvidence; // TODO implement this on conversation end
    
    public Sprite PersonIcon;
}

[System.Serializable]
public class ConversationLine
{
    public string LineText;
    public string PersonName;
    public Sprite PersonIcon;
    public AudioClip TalkSound;
    public bool isPersonOnLeftTalking;
}

[System.Serializable]
public class ConversationChoice
{
    public string ChoiceText;
    public ItemData RequiredItem;
    public ConversationNode NextNode;

    [Header("You need at least one of these")]
    public ItemData[] RequiredItemList;

    [Header("Only for deductions")]
    public int EndingPoints;
    public EndingType endingType;
}

public enum EndingType
{
    None, Bad, Silly, Good, SelfIncriminate
}
