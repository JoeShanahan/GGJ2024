using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected string _interactText = "Interact";

    public string InteractText => _interactText;

    public virtual void Interact(Vector3 fromPos)
    {
        
    }
}
