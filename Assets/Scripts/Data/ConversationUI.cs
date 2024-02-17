using System.Collections;
using System.Collections.Generic;
using RedBlueGames.Tools.TextTyper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

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
    private TMP_Text _nameText;

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
    
    private void Start()
    {
        _typer.PrintCompleted.AddListener(() => OnTyperFinish());
    }

    private void OnTyperFinish()
    {
        bool isFinalLine = _lineNumber >= _currentNode.DialogueLines.Count - 1;

        if (_currentNode.DialogueLines[_lineNumber].ClueMentioned != null)
        {
            _evidence.AddEvidence(_currentNode.DialogueLines[_lineNumber].ClueMentioned);
        }
        
        if (isFinalLine)
        {
            StartCoroutine(CreateResponsesWithDelay());
        }
    }

    private IEnumerator CreateResponsesWithDelay()
    {
        // We need a delay to stop the button press to advance the conversation also choose an option
        yield return new WaitForSeconds(0.1f);

        CreateDialogueResponses(_currentNode);
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
        bool haveSelectedSomething = false;

        for (int i = 0; i < _choiceButtons.Count; i++)
        {
            TextMeshProUGUI buttonText = _choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            
            if (i < node.Choices.Count)
             {
                ConversationChoice currentChoice = node.Choices[i];
               
                if (_evidence.CanShowThisChoice(currentChoice))
                {
                    buttonText.text = node.Choices[i].ChoiceText;
                    _choiceButtons[i].gameObject.SetActive(true);

                    if (haveSelectedSomething == false)
                    {
                        haveSelectedSomething = true;
                        EventSystem.current.SetSelectedGameObject(_choiceButtons[i].gameObject);
                    }
                }
                else
                {
                    _choiceButtons[i].gameObject.SetActive(false);
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
        if (isFinalLine && DoHaveChoices(node))
        {
            // Do nothing, we're waiting on a choice to be made
        }
        else if (isFinalLine)
        {
            _overlord.EndConversation(_currentNode);
        }
        else
        {
            _lineNumber++;
            SetTalkerPortraits();
            LightUpTalker(node);
            _typer.TypeText(node.DialogueLines[_lineNumber].LineText);
            _nameText.text = node.DialogueLines[_lineNumber].Person.CharacterName + ":";
        }
    }
    
    public void SetTalkerPortraits()
    {
        if (_lineNumber == 0)
        {
            _leftPersonPortrait.sprite = _currentNode.InitialLeftPerson.Sprite;
            _rightPersonPortrait.sprite = _currentNode.InitialRightPerson.Sprite;
        } 
        else
        { 
        if (_currentNode.DialogueLines[_lineNumber].isPersonOnLeftTalking)
            {
                _leftPersonPortrait.sprite = _currentNode.DialogueLines[_lineNumber].Person.Sprite;
            } 
            else
            {
                _rightPersonPortrait.sprite = _currentNode.DialogueLines[_lineNumber].Person.Sprite;
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
            _overlord.EndConversation(_currentNode);
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

    

