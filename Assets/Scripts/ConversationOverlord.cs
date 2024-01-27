using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RedBlueGames.Tools.TextTyper;

public class ConversationOverlord : MonoBehaviour
{
    [SerializeField] public ConversationNode TestNode;
    Canvas canvas;

    [Header("ConversationBox")]
    [SerializeField] TextMeshProUGUI convoText;
    public bool convoStarted;
    public int lineNumber;
    TextTyper typer;




    [Header("DialogueResponses")]
    [SerializeField] GameObject[] answerButtons;

    public void Start()
    {
        GameObject tempObject;
        tempObject = GameObject.Find("ConvoCanvas");
        canvas = tempObject.GetComponent<Canvas>();
        typer = FindObjectOfType<TextTyper>();
        canvas.gameObject.SetActive(false);
    }


    public void StartConversation(ConversationNode node)
    {
        ShowConversationCanvas(true);
        typer.TypeText(node.DialogueLines[lineNumber].LineText);
        //convoText.text = node.DialogueLines[lineNumber].LineText;
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
            typer.TypeText(node.DialogueLines[lineNumber].LineText);
            //convoText.text = node.DialogueLines[lineNumber].LineText;
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
                if (typer.IsSkippable())
                {
                    typer.Skip();
                } else
                {
                    ContinueConversation(TestNode);
                }
 
            }
            
        }
    }

    
}
