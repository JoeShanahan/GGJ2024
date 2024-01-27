using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConversationNode : ScriptableObject
{
    public List<ConversationLine> DialogueLines;
    public List<ConversationChoice> Choices;
    public ItemData unlockedEvidence;
}

[System.Serializable]
public class ConversationLine
{
    public string LineText;
    public string PersonName;
    public Sprite PersonIcon;
    public AudioClip TalkSound;
}

[System.Serializable]
public class ConversationChoice
{
    public string ChoiceText;
    public ItemData RequiredItem;
    public ConversationNode NextNode;
    public int EndingPoints;
    public EndingType endingType;
}

public enum EndingType
{
    None, Bad, Silly, Good
}
