using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private bool _isEndingShowing;

    public void ShowEnding(EndingData ending)
    {
        if (_isEndingShowing)
            return;

        _isEndingShowing = true;
        _screen.ShowEnding(ending);
    }

    private void Update()
    {
        if (_isEndingShowing && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }

    // This might be the single worst function I've ever written in my life (-Joe)
    private void SetDialogueText()
    {
        ConversationLine line1 = _dynamicEndNode.DialogueLines[1];
        ConversationLine line2 = _dynamicEndNode.DialogueLines[2];

        if (Deductions.HasFlag(Deduction.BalloonStabbed))
        {
            line1.LineText = "The victim was <color=red>stabbed with a balloon</color>.";

            if (Deductions.HasFlag(Deduction.DinoCurse))
            {
                line2.LineText = "A feat that might seem impossible if it weren't for the fact they had also been <color=red>cursed by a dinosaur</color>";
            }
            else if (Deductions.HasFlag(Deduction.MummyCurse))
            {
                line2.LineText = "A feat that might seem impossible, but anything is possible after activating the <color=red>curse of the mummy</color>";
            }
            else if (Deductions.HasFlag(Deduction.JealousStaff))
            {
                line2.LineText = "What's more, this horrible crime was committed by <color=red>a member of staff</color> from this very museum";
            }
            else if (Deductions.HasFlag(Deduction.JealousSomeone))
            {
                line2.LineText = "Such a skill is limited to only a few highly trained professionals.";
            }
        }
        else if (Deductions.HasFlag(Deduction.DaggerStabbed))
        {
            line1.LineText = "The victim was <color=red>stabbed with an antique dagger</color>, one that's missing from the museum";

            if (Deductions.HasFlag(Deduction.DinoCurse))
            {
                line2.LineText = "The dagger found its way into the victim after they angered the <color=red>bones of the dinosaur</color>";
            }
            else if (Deductions.HasFlag(Deduction.MummyCurse))
            {
                line2.LineText = "This happened around the same time that a <color=red>mummy went missing</color> from a storage room in the museum";
            }
            else if (Deductions.HasFlag(Deduction.JealousStaff))
            {
                line2.LineText = "The person responsible is someone who <color=red>works here at the museum</color>";
            }
            else if (Deductions.HasFlag(Deduction.JealousSomeone))
            {
                line2.LineText = "Naturally we would supsect the museum staff, but that's part of the scheme. In actual fact it was <color=red>someone else</color>";
            }
        }
        else if (Deductions.HasFlag(Deduction.CyanidePoisoned))
        {
            line1.LineText = "The victim was <color=red>poisoned with cyanide</color>, hidden inside an almond cake";

            if (Deductions.HasFlag(Deduction.DinoCurse))
            {
                line2.LineText = "Truly the work of a sinister force, a <color=red>curse</color> in fact";
            }
            else if (Deductions.HasFlag(Deduction.MummyCurse))
            {
                line2.LineText = "Truly the work of a sinister force, a <color=red>curse</color> in fact";
            }
            else if (Deductions.HasFlag(Deduction.JealousStaff))
            {
                line2.LineText = "The victim will have only accepted cake from someone they knew well, a fellow <color=red>member of staff</color>";
            }
            else if (Deductions.HasFlag(Deduction.JealousSomeone))
            {
                line2.LineText = "Or at least that's what <color=red>someone</color> would like us to believe";
            }
        }
        else if (Deductions.HasFlag(Deduction.PlasticPoisoned))
        {
            line1.LineText = "The victim died after a sudden build up of <color=red>microplastics</color> in theif bloodstream";
             
            if (Deductions.HasFlag(Deduction.DinoCurse))
            {
                line2.LineText = "This is a <color=red>curse</color> that can affect anyone in our modern world";
            }
            else if (Deductions.HasFlag(Deduction.MummyCurse))
            {
                line2.LineText = "This is a <color=red>curse</color> that can affect anyone in our modern world";
            }
            else if (Deductions.HasFlag(Deduction.JealousStaff))
            {
                line2.LineText = "The <color=red>staff</color> didn't like him very much, but it's unclear what their role was";
            }
            else if (Deductions.HasFlag(Deduction.JealousSomeone))
            {
                line2.LineText = "That should make the issue of who is responsible very clear";
            }
        }
        else if (Deductions.HasFlag(Deduction.HeartAttack))
        {
            line1.LineText = "The victim died of a <color=red>heart attack</color>, prompted by the terrible storm we had tonight";
             
            if (Deductions.HasFlag(Deduction.DinoCurse))
            {
                line2.LineText = "But this was no ordinary storm, it was caused by a <color=red>Dinosaur Curse</color>";
            }
            else if (Deductions.HasFlag(Deduction.MummyCurse))
            {
                line2.LineText = "But this was no ordinary storm, it was caused by a <color=red>Mummy's Curse</color>";
            }
            else if (Deductions.HasFlag(Deduction.JealousStaff))
            {
                line2.LineText = "Given how universally disliked the victim was by the staff, we can't rule out foul play";
            }
            else if (Deductions.HasFlag(Deduction.JealousSomeone))
            {
                line2.LineText = "It's not clear that anyone else was involved";
            }
        }
        else if (Deductions.HasFlag(Deduction.OldAge))
        {
            line1.LineText = "The victim died of <color=red>Old Age</color>.";
             
            if (Deductions.HasFlag(Deduction.DinoCurse))
            {
                line2.LineText = "But such accelerated aging isn't normally possible, not without the effects of an <color=red>ancient curse</color>";
            }
            else if (Deductions.HasFlag(Deduction.MummyCurse))
            {
                line2.LineText = "But such accelerated aging isn't normally possible, not without the effects of an <color=red>ancient curse</color>";
            }
            else if (Deductions.HasFlag(Deduction.JealousStaff))
            {
                line2.LineText = "None of the staff here particularly liked him, so I doubt he'll be missed";
            }
            else if (Deductions.HasFlag(Deduction.JealousSomeone))
            {
                line2.LineText = "It's sad, but it happens all the time, so don't be sad for too long.";
            }
        }
    }

    public void ConstructDynamicNode()
    {
        _dynamicEndNode.Choices = new List<ConversationChoice>();

        SetDialogueText();

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
                ChoiceText = "...I don't know",
                NextNode = _defaultNode
            };

            _dynamicEndNode.Choices.Add(convChoice);
        }
    }
}
