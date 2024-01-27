using System.Collections;
using System.Collections.Generic;
using RedBlueGames.Tools.TextTyper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the UI for the conversation, including moving through the text
/// This gameobject is inactive when not in use
/// </summary>
public class ConversationUI : MonoBehaviour
{
    public int LineNumber;
    public bool ConvoStarted;

    [SerializeField]
    private List<Button> _choiceButtons;

    [SerializeField]
    private TextTyper _typer;

    [SerializeField]
    private ConversationOverlord _overlord;

    private int _lineNumber;
    private ConversationNode _currentNode;

    private bool HasGotItem(ItemData data) 
    {
        return false;
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void SetNewNode(ConversationNode node)
    {
        _currentNode = node;
        _lineNumber = -1;
        HideAllButtons();
        ContinueConversation(node);
    }

    private void HideAllButtons()
    {
        foreach (Button btn in _choiceButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void CreateDialogueResponses(ConversationNode node) 
    {
        
        for (int i = 0; i < _choiceButtons.Count; i++)
        {
            TextMeshProUGUI buttonText = _choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            
            if (i < node.Choices.Count)
             {
                ConversationChoice currentChoice = node.Choices[i];
                if (currentChoice.RequiredItem != null && HasGotItem (currentChoice.RequiredItem) == false)
                {
                    _choiceButtons[i].gameObject.SetActive(false);

                } else
                {
                    buttonText.text = node.Choices[i].ChoiceText;
                    _choiceButtons[i].gameObject.SetActive(true);
                }
                
            }
            else
            {
                _choiceButtons[i].gameObject.SetActive(false);
            }           
        }
    }

    public void ContinueConversation(ConversationNode node)
    {
        bool isFinalLine = _lineNumber >= node.DialogueLines.Count - 1;
        bool doHaveChoices = node.Choices.Count > 0;

        if (isFinalLine && doHaveChoices)
        {
            CreateDialogueResponses(node);
        }
        else if (isFinalLine)
        {
            _overlord.EndConversation();
        }
        else
        {
            _lineNumber ++;
            _typer.TypeText(node.DialogueLines[_lineNumber].LineText);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_typer.IsSkippable())
            {
                _typer.Skip();
            } 
            else
            {
                ContinueConversation(_currentNode);
            }
        }
    }

    public void ButtonPress(int buttonIndex)
    {
        ConversationChoice playerSelection = _currentNode.Choices[buttonIndex];

        // TODO if player selection has node, SetNewNode
        // TODO else, end conversation 

        Debug.Log($"You pressed {buttonIndex}");
        _overlord.EndConversation();
    }
}

    

