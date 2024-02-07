using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private List<Image> _itemIcons;

    [SerializeField]
    public List<ItemData> _foundEvidence;

    [SerializeField]
    public TextMeshProUGUI _itemText;

    public void MakeItemShowInInventory (ItemData data)
    {
        Debug.Log(_foundEvidence.Count - 1);
        
        _itemIcons[(_foundEvidence.Count - 1)].sprite = data.Icon;
        var tempColor = _itemIcons[_foundEvidence.Count - 1].color;
        tempColor.a = 1f;
        _itemIcons[_foundEvidence.Count - 1].color = tempColor;
    }

    public void DisplayItemDescription(int index) 
    {
        Debug.Log(index);
        Debug.Log(_foundEvidence[index].Description);
        _itemText.text = _foundEvidence[index].Description;
    }
}
