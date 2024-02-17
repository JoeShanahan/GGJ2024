using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RedBlueGames.Tools.TextTyper;

/// <summary>
/// This class handles the logic of starting and ending conversations
/// This gameobject will always be active
/// </summary>
public class ConversationOverlord : MonoBehaviour
{
    [SerializeField] 
    public ConversationNode TestNode;

    public bool isConvoActive;

    [SerializeField]
    private ConversationUI _ui;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private ProgressionManager _progress;

    [SerializeField]
    private EndingManager _ending;

    public void Start()
    {
        _ui.gameObject.SetActive(false);
    }

    public void StartConversation(ConversationNode node)
    {
        _ui.ShowUI();
        _ui.SetNewNode(node);
        _player.DeactivateControls();
        
        isConvoActive = true;
    }

    public void EndConversation(ConversationNode node)
    {
        if (node.Ending != null)
        {
            _ending.ShowEnding(node.Ending);
            return;
        }

        _ui.HideUI();
        _player.ReactivateControls();

        if (node.DoesAdvanceProgression)
        {
            _progress.AdvanceToNextPhase();
        }
    
        isConvoActive = false;
    }

    

    
}
