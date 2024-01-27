using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingPerson : Interactable
{
    [SerializeField]
    private ConversationNode _convoStart;
    
    public override void Interact(Vector3 fromPos)
    {

    }
}
