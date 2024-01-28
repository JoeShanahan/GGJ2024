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

    public void RefreshCurrentFocus()
    {
        if (_inRange.Count == 0)
        {
            OnFocusChange?.Invoke(null);
        }
        else
        {
            if (_inRange.Last() == null || _inRange.Last().IsInteractable == false)
            {
                _inRange.RemoveAt(_inRange.Count - 1);
                RefreshCurrentFocus();
                return;
            }

            OnFocusChange?.Invoke(_inRange.Last());
        }
    }
}
