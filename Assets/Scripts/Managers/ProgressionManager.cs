using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField]
    private List<PhaseInfo> _phases;

    [SerializeField]
    private TMP_Text _instructionText;

    [SerializeField]
    private RectTransform _instructionBox;

    [SerializeField]
    private PhaseChangeUI _phaseUI;

    private int _numCluesRemain;
    
    private Dictionary<EndingType, int> _endingPoints = new()
    {
        { EndingType.Bad, 0 },
        { EndingType.Good, 0 },
        { EndingType.Silly, 0 },
        { EndingType.SelfIncriminate, 0 },
    };

    private int _currentPhase = -1;

    public int CurrentPhase => _currentPhase;

    private HashSet<ConversationChoice> _previousAnswers = new();

    public void MakeChoice(ConversationChoice choice)
    {
        // we don't want to add the points twice
        if (_previousAnswers.Contains(choice))
            return;

        if (choice.EndingPoints == 0 || choice.endingType == EndingType.None)
            return;

        _endingPoints[choice.endingType] += choice.EndingPoints;
        Debug.Log($"Earned {choice.EndingPoints} points towards the {choice.endingType} ending - now on {_endingPoints[choice.endingType]}");
    }

    private void Start()
    {
        _instructionText.text = "Talk to the inspector";

        
        foreach (PhaseInfo pInfo in _phases)
        {
            foreach (Transform t in pInfo.ObjectsToShow)
            {
                t.gameObject.SetActive(false);
            }
        }

        foreach (PhaseInfo pInfo in _phases)
        {
            foreach (Transform t in pInfo.ObjectsToHide)
            {
                t.gameObject.SetActive(true);
            }
        }
    }

    public void AdvanceToNextPhase()
    {
        if (_currentPhase >= 0)
        {
            foreach (EvidenceItem i in _phases[_currentPhase].Evidence)
            {
                i.CleanUp();
            }
        }
        
        _currentPhase ++;
        _phaseUI.StartNewPhase(_phases[_currentPhase], _currentPhase + 1);
        
        if (_currentPhase >= _phases.Count)
        {
            GoToConclusion();
            return;
        }

        foreach (Transform t in _phases[_currentPhase].ObjectsToHide)
        {
            t.gameObject.SetActive(false);
        }

        
        foreach (Transform t in _phases[_currentPhase].ObjectsToShow)
        {
            t.gameObject.SetActive(true);
        }

        _numCluesRemain = _phases[_currentPhase].MinimumRequiredEvidence;
        RefreshInstructions();
    }
    
    private void RefreshInstructions()
    {
        if (_numCluesRemain > 1)
            _instructionText.text = $"Find at least {_numCluesRemain} more clues";
        else if (_numCluesRemain == 1)
            _instructionText.text = $"Find at least 1 more clue";
        else
            _instructionText.text = $"Keep searching or talk to the inspector";
            
    }

    private void GoToConclusion()
    {

    }
}

[System.Serializable]
public class PhaseInfo
{
    public int MinimumRequiredEvidence;
    public Transform[] ObjectsToHide;
    public Transform[] ObjectsToShow;
    public List<EvidenceItem> Evidence;
    public string Title;
    public string SubTitle;
}
