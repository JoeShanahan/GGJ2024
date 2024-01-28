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
    private Image _detectivePortrait;

    [SerializeField]
    private Image _otherSpeaker;

    [SerializeField]
    private TextTyper _typer;

    [SerializeField]
    private ConversationOverlord _overlord;

    [SerializeField]
    private EvidenceManager _evidence;

    
    private int _lineNumber;
    private ConversationNode _currentNode;

    public void ShowUI()
    {
        // TODO Animation
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        // TODO Animation
        gameObject.SetActive(false);
    }

    public void SetNewNode(ConversationNode node)
    {
        _currentNode = node;
        _lineNumber = -1;
        HideAllButtons();
        ContinueConversation(node);
    }

    public void SetTalkerPortrait()
    {
        
        _otherSpeaker.GetComponent<Image>();

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
            SetTalkerPortrait();
            GetOtherTalker();
            LightUpTalker(node);
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


    public void GetOtherTalker()
    {
        _otherSpeaker.sprite = _currentNode.PersonIcon;
    }
    public void LightUpTalker(ConversationNode node)
    {
        if (node.DialogueLines[_lineNumber].isPersonOnLeftTalking)
        {
            _detectivePortrait.color = Color.white;
           _otherSpeaker.color = Color.gray;
        } else
        {
            _detectivePortrait.color = Color.gray;
            _otherSpeaker.color = Color.white;
        }
    }
}

    

