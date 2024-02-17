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

    [SerializeField]
    private EndingManager _ending;

    private int _cluesFoundThisPhase;
    private int _totalCluesThisPhase;
    
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

        _ending.Deductions |= choice.DeductionFlags;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Application.platform != RuntimePlatform.WebGLPlayer)
            Application.Quit();
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
        
        if (_currentPhase >= _phases.Count)
        {
            return;
        }

        _phaseUI.StartNewPhase(_phases[_currentPhase], _currentPhase + 1);

        foreach (Transform t in _phases[_currentPhase].ObjectsToHide)
        {
            t.gameObject.SetActive(false);
        }

        
        foreach (Transform t in _phases[_currentPhase].ObjectsToShow)
        {
            t.gameObject.SetActive(true);
        }

        _cluesFoundThisPhase = 0;
        _totalCluesThisPhase = _phases[_currentPhase].Evidence.Count;
        RefreshInstructions();
    }

    public void OnClueFound()
    {
        _cluesFoundThisPhase ++;
        RefreshInstructions();
    }
    
    public void RefreshInstructions()
    {
        if (_cluesFoundThisPhase == 0)
            _instructionText.text = $"Look for clues";
        else if (_cluesFoundThisPhase < _totalCluesThisPhase)
            _instructionText.text = $"Keep searching or talk to the inspector";
        else
            _instructionText.text = $"Talk to the inspector";
            
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
