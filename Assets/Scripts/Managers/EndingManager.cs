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
    }

    public Deduction Deductions;
    public EndingData Ending;

    [SerializeField]
    private EvidenceManager _evidence;

    [SerializeField]
    private ConversationNode _dynamicEndNode;

    [SerializeField]
    private List<EndingRequirement> _endings;

    [SerializeField]
    private ConversationNode _defaultNode;

    [SerializeField]
    private EndingScreen _screen;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ShowEnding(Ending);
    }

    public void ShowEnding(EndingData ending)
    {
        _screen.ShowEnding(ending);
    }

    public void ConstructDynamicNode()
    {
        _dynamicEndNode.Choices = new List<ConversationChoice>();

        foreach (EndingRequirement req in _endings)
        {
            bool isValid = true;

            foreach (Deduction ded in req.RequiredDeductions)
            {
                if (Deductions.HasFlag(ded) == false)
                    isValid = false;

            }

            foreach (ItemData itm in req.RequiredEvidence)
            {
                if (_evidence.HasFoundItem(itm) == false)
                    isValid = false;
            }

            if (isValid == false)
                continue;

            var convChoice = new ConversationChoice()
            {
                ChoiceText = req.ChoiceText,
                NextNode = req.ConvNode
            };

            _dynamicEndNode.Choices.Add(convChoice);
        }

        if (_dynamicEndNode.Choices.Count == 0)
        {
            
            var convChoice = new ConversationChoice()
            {
                ChoiceText = "I don't know",
                NextNode = _defaultNode
            };

            _dynamicEndNode.Choices.Add(convChoice);
        }
    }
}
