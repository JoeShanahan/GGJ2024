using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConversationNode : ScriptableObject
{
    public List<ConversationLine> DialogueLines;
    public List<ConversationChoice> Choices;
}

[System.Serializable]
public class ConversationLine
{
    public string LineText;
    public string PersonName;
    public Sprite PersonIcon;
    public AudioClip TalkSound;

    public string GetNextLine()
    {
        return LineText;
    }
}

[System.Serializable]
public class ConversationChoice
{
    public string ChoiceText;
    public ItemData RequiredItem;
    public ConversationNode NextNode;
}
