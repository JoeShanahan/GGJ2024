using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public string ShortDescription;
    
    [TextArea(1, 5)]
    public string Description;
    public Sprite Icon;
}
