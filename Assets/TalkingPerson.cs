using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingPerson : Interactable
{
    [SerializeField]
    private ConversationNode _myConversationNode;

    [SerializeField]
    ConversationOverlord _convoOverlord;
    
    public override void Interact(Vector3 fromPos)
    {
        _convoOverlord.StartConversation(_myConversationNode);
    }
}
