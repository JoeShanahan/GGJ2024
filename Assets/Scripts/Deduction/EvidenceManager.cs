using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> _foundEvidence;

    [SerializeField]
    private ClueToast _clueToast;

    public int EvidenceCount => _foundEvidence.Count;

    public bool HasFoundItem(ItemData data)
    {
        return _foundEvidence.Contains(data);
    }

    public void AddEvidence(ItemData data)
    {
        if (_foundEvidence.Contains(data) == false)
        {
            _foundEvidence.Add(data);
            _clueToast.ShowFoundClue(data);
        }
    }
}

