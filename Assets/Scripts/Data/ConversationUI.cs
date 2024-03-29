using System.Collections;
using System.Collections.Generic;
using RedBlueGames.Tools.TextTyper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private Image _leftPersonPortrait;

    [SerializeField]
    private Image _rightPersonPortrait;

    [SerializeField]
    private TextTyper _typer;

    [SerializeField]
    private ConversationOverlord _overlord;

    [SerializeField]
    private EvidenceManager _evidence;

    PlayerInputActions _input;

    private int _lineNumber;
    private ConversationNode _currentNode;

    public void ShowUI()
    {
        if (_input == null)
        {
            _input = new();
            _input.Movement.Item.performed += (ctx) => AdvanceText();
        }

        // TODO Animation
        gameObject.SetActive(true);
        _input.Enable();
    }

    public void HideUI()
    {
        // TODO Animation
        gameObject.SetActive(false);
        _input.Disable();
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
               
                if (_evidence.CanShowThisChoice(currentChoice))
                {
                    Debug.Log("creating responses1");
                    _choiceButtons[i].gameObject.SetActive(false);

                } else
                {
                    Debug.Log("creatingresponses2");
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

    private bool DoHaveChoices(ConversationNode node)
    {
        if (node.Choices.Count == 0)
            return false;
        
        
        foreach (ConversationChoice choice in node.Choices)
        {
            if (_evidence.CanShowThisChoice(choice))
                return true;
        }

        return false;
    }

    public void ContinueConversation(ConversationNode node)
    {
        bool isFinalLine = _lineNumber >= node.DialogueLines.Count - 1;
        Debug.Log(isFinalLine);
        if (isFinalLine && DoHaveChoices(node))
        {
            Debug.Log("failure");
            CreateDialogueResponses(node);
        }
        else if (isFinalLine)
        {
            Debug.Log("whatever");
            _overlord.EndConversation();
        }
        else
        {
            _lineNumber++;
            SetTalkerPortraits();
            LightUpTalker(node);
            _typer.TypeText(node.DialogueLines[_lineNumber].LineText);
        }
    }
    
    public void SetTalkerPortraits()
    {
        if (_lineNumber == 0)
        {
            _leftPersonPortrait.sprite = _currentNode.InitialLeftPersonIcon;
            _rightPersonPortrait.sprite = _currentNode.InitialRightPersonIcon;
        } 
        else
        { 
        if (_currentNode.DialogueLines[_lineNumber].isPersonOnLeftTalking)
            {
                _leftPersonPortrait.sprite = _currentNode.DialogueLines[_lineNumber].PersonIcon;
            } 
            else
            {
                _rightPersonPortrait.sprite = _currentNode.DialogueLines[_lineNumber].PersonIcon;
            }
        }
        
    }

    public void AdvanceText()
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

    public void ButtonPress(int buttonIndex)
    {
        ConversationChoice playerSelection = _currentNode.Choices[buttonIndex];
        FindObjectOfType<ProgressionManager>().MakeChoice(playerSelection);

        if (playerSelection.NextNode != null)
        {
            SetNewNode(playerSelection.NextNode);
        }
        else
        {
            _overlord.EndConversation();
        }
    }

    public void LightUpTalker(ConversationNode node)
    {
        if (node.DialogueLines[_lineNumber].isPersonOnLeftTalking)
        {
            _leftPersonPortrait.color = Color.white;
           _rightPersonPortrait.color = Color.gray;
        } else
        {
            _leftPersonPortrait.color = Color.gray;
            _rightPersonPortrait.color = Color.white;
        }
    }
}

    

