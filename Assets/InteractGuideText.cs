using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractGuideText : W2C
{
    [SerializeField]
    private TMP_Text _text;

    private void Start()
    {
        Initialize(FindAnyObjectByType<Canvas>(), Camera.main);
    }

    public void SetInteractable(Interactable item)
    {
        gameObject.SetActive(item != null);
        
        if (item != null)
        {
            _text.text = item.InteractText;
            SetPosition(item.transform, Vector3.up);
        }
    }
}
