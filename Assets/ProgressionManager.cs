using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField]
    private List<PhaseInfo> _phases;

    private int _currentPhase;

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
