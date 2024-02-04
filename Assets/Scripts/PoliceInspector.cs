using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceInspector : Interactable
{
    [SerializeField]
    private ConversationNode[] _conversationNodes;

    [SerializeField]
    private Transform[] _standPositions;

    [SerializeField]
    ConversationOverlord _convoOverlord;

    [SerializeField]
    private ProgressionManager _progression;
    
    public override void Interact(Vector3 fromPos)
    {
        int currentPhase = _progression.CurrentPhase + 1;
        ConversationNode node = _conversationNodes[currentPhase];
        
        if (node != null)
            _convoOverlord.StartConversation(node);
    }
}
