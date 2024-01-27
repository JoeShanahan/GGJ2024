using UnityEngine;

public class ConversationOverlord : MonoBehaviour
{
    [SerializeField] public ConversationNode TestNode;
    Canvas canvas;
    public bool convoStarted;
    ConversationController conversationController;
    public int lineNumber;




    public void Start()
    {
        conversationController = FindObjectOfType<ConversationController>();
        GameObject tempObject;
        tempObject = GameObject.Find("ConvoCanvas");
        canvas = tempObject.GetComponent<Canvas>();
        canvas.gameObject.SetActive(false);
    }


    public void StartConversation(ConversationNode node)
    {
        ShowConversationCanvas(true);
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
            Debug.Log(node.DialogueLines[lineNumber].LineText);
        }
        else
        {
            EndConversation();
        }

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
