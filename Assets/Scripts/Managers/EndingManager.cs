using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum Deduction
{
    None = 0,
    BalloonStabbed = 1,
    DaggerStabbed = 2,
    CyanidePoisoned = 4,
    PlasticPoisoned = 8,
    HeartAttack = 16,
    OldAge = 32,
    DinoCurse = 64,
    MummyCurse = 128,
    JealousStaff = 256,
    JealousSomeone = 512
}


public class EndingManager : MonoBehaviour
{
    [System.Serializable]
    public class EndingRequirement
    {
        public string Description;
        public string ChoiceText;
        public Deduction[] RequiredDeductions;
        public ItemData[] RequiredEvidence;
        public ConversationNode ConvNode;
        public EndingData Ending;
    }

    public Deduction Deductions;
    public EndingData Ending;

    [SerializeField]
    private ConversationNode _dynamicEndNode;

    [SerializeField]
    private List<EndingRequirement> _endings;

    [SerializeField]
    private ConversationNode _defaultNode;

    [SerializeField]
    private EndingData _defaultEnding;
}
