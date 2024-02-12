using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> _foundEvidence;

    [SerializeField]
    private ClueToast _clueToast;

    [SerializeField]
    private ProgressionManager _progress;

    public int EvidenceCount => _foundEvidence.Count;

    public bool HasFoundItem(ItemData data)
    {
        return _foundEvidence.Contains(data);
    }

    public bool CanShowThisChoice(ConversationChoice choice)
    {
        // If we don't need an item, then we're good to show
        if (choice.RequiredItemList.Length == 0)
            return true;
        if (choice.needAll)
        {
            foreach (ItemData evidence in choice.RequiredItemList)
            {
                // If we have any one of those items, then we can show the option
                if (!_foundEvidence.Contains(evidence))
                    return false;
            }

            return true;
        }
        else
        {
            foreach (ItemData evidence in choice.RequiredItemList)
            {
                // If we have any one of those items, then we can show the option
                if (_foundEvidence.Contains(evidence))
                    return true;
            }
        }
            

        return false;
    }

    public void AddEvidence(ItemData data)
    {
        if (_foundEvidence.Contains(data) == false)
        {
            _foundEvidence.Add(data);
            _clueToast.ShowFoundClue(data);
            _progress.OnClueFound();
        }
    }
}

