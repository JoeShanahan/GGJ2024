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

    [Header("ConversationBox")]
    [SerializeField] TextMeshProUGUI convoText;
    public bool isConvoActive;
    public int lineNumber;

    [SerializeField]
    private ConversationUI _ui;

    [Header("DialogueResponses")]
    public int choiceNumber;
    [SerializeField] GameObject[] answerButtons;

    public void Start()
    {
        _ui.gameObject.SetActive(false);
    }

    public void StartConversation(ConversationNode node)
    {
        _ui.ShowUI();
        _ui.SetNewNode(node);
        
        isConvoActive = true;
        // TODO disable player controls
    }

    public void EndConversation()
    {
        _ui.HideUI();
        //ShowConversationCanvas(false);
        isConvoActive = false;
        // TODO enable the player controls
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!isConvoActive)
            {
                StartConversation(TestNode);
            } 
        }
    }

    
}
