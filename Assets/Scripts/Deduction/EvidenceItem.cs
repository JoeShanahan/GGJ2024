using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceItem : Interactable
{
    [SerializeField]
    private ItemData _data;
    [SerializeField] private GameObject particles;

    public override bool IsInteractable => _isInteractable;

    private bool _isInteractable = true;

    public override void Interact(Vector3 fromPos)
    {
        FindObjectOfType<EvidenceManager>().AddEvidence(_data);
        Debug.Log(_data);
        _isInteractable = false;
        Destroy(particles);
    }
}
