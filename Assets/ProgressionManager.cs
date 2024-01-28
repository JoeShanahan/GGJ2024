using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField]
    private List<PhaseInfo> _phases;
    
    private Dictionary<EndingType, int> _endingPoints = new()
    {
        { EndingType.Bad, 0 },
        { EndingType.Good, 0 },
        { EndingType.Silly, 0 },
        { EndingType.SelfIncriminate, 0 },
    };

    private int _currentPhase;

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
        foreach (PhaseInfo phase in _phases)
        {
            phase.RoomCoverMat?.DOFade(1, 0.1f);
        }
    }

    public void AdvanceToNextPhase()
    {
        _currentPhase ++;
        
        if (_currentPhase >= _phases.Count)
        {
            GoToConclusion();
            return;
        }

        foreach (Transform t in _phases[_currentPhase].RoomsToUnlock)
        {
            t.gameObject.SetActive(true);
        }

        foreach (Transform t in _phases[_currentPhase].BlockersToHide)
        {
            t.gameObject.SetActive(false);
        }

        _phases[_currentPhase].RoomCoverMat.DOFade(0, 1f);
    }

    private void GoToConclusion()
    {

    }
}

[System.Serializable]
public class PhaseInfo
{
    public int MinimumRequiredEvidence;
    public Transform RoomsToUnlock;
    public Material RoomCoverMat;
    public Transform BlockersToHide;
}
