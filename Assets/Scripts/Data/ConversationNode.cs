using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConversationNode : ScriptableObject
{
    public List<ConversationLine> DialogueLines;
    public List<ConversationChoice> Choices;
    public CharacterData InitialLeftPerson;
    public CharacterData InitialRightPerson;
    public bool DoesAdvanceProgression;
    public EndingData Ending;
}

[System.Serializable]
public class ConversationLine
{
    public string LineText;
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
    public Deduction DeductionFlags;
}

public enum EndingType
{
    None, Bad, Silly, Good, SelfIncriminate
}
