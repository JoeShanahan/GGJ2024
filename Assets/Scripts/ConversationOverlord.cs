using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationOverlord : MonoBehaviour
{
    [SerializeField] public ConversationNode TestNode;
    Canvas canvas;

    [Header("ConversationBox")]
    [SerializeField] TextMeshProUGUI convoText;
    public bool convoStarted;
    public int lineNumber;

    [Header("DialogueResponses")]
    [SerializeField] GameObject[] answerButtons;

    public void Start()
    {
        GameObject tempObject;
        tempObject = GameObject.Find("ConvoCanvas");
        canvas = tempObject.GetComponent<Canvas>();
        canvas.gameObject.SetActive(false);
    }


    public void StartConversation(ConversationNode node)
    {
        ShowConversationCanvas(true);
        convoText.text = node.DialogueLines[lineNumber].LineText;
        Debug.Log(node.DialogueLines[lineNumber].LineText);
        convoStarted = true;
    }

    public void ShowConversationCanvas(bool Talking)
    {
        canvas.gameObject.SetActive(Talking);
    }

    public void EndConversation()
    {
        ShowConversationCanvas(false);
        lineNumber = 0;
        convoStarted = false;
    }

    public void ContinueConversation(ConversationNode node)
    {
        if (lineNumber < (TestNode.DialogueLines.Count-1))
        {
            lineNumber++;
            convoText.text = node.DialogueLines[lineNumber].LineText;
            Debug.Log(node.DialogueLines[lineNumber].LineText);
        }
        else
        {
            EndConversation();
        }

    }

    public void ShowText()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!convoStarted)
            {
                StartConversation(TestNode);

            } 
            else
            {
                ContinueConversation(TestNode);
            }
            
        }
    }

    
}
