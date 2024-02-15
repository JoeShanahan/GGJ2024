using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryOverlord : MonoBehaviour
{
    [SerializeField]
    private InventoryManager _inventory;

    

    public void ShowInventory()
    {
        _inventory.ShowUI();
        bool haveSelectedSomething = false;

        if (haveSelectedSomething == false)
        {
            haveSelectedSomething = true;
            EventSystem.current.SetSelectedGameObject(_inventory._itemIcons[0].gameObject);
        }
    }

   
    public void HideInventory()
    {
        _inventory.ExitTheInventory();
        
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
