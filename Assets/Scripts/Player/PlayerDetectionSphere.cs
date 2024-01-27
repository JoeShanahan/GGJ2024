using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDetectionSphere : MonoBehaviour
{
    public event Action<Interactable> OnFocusChange;

    private List<Interactable> _inRange = new();

    public void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();

        if (interactable != null && _inRange.Contains(interactable) == false)
        {
            _inRange.Add(interactable);
            RefreshCurrentFocus();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();

        if (interactable != null && _inRange.Contains(interactable))
        {
            _inRange.Remove(interactable);
            RefreshCurrentFocus();
        }
    }

    private void RefreshCurrentFocus()
    {
        if (_inRange.Count == 0)
        {
            OnFocusChange?.Invoke(null);
            Debug.Log("nada");
        }
        else
        {
            OnFocusChange?.Invoke(_inRange.Last());
            Debug.Log("Set " + _inRange.Last().gameObject.name);
        }
    }
}
