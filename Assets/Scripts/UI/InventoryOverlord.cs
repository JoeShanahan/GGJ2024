using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOverlord : MonoBehaviour
{
    [SerializeField]
    private InventoryManager _inventory;

    public void ShowInventory()
    {
        _inventory.ShowUI();
    }

    public void HideInventory()
    {
        _inventory.HideUI();
    }

    private void Update()
    {
        // TODO remove this
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!_inventory.isActiveAndEnabled)
            {
                ShowInventory();
            } else
            {
                HideInventory();
            }
        }
    }
}
