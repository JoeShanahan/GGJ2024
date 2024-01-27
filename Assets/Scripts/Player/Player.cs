using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

[RequireComponent(typeof(PersonMovement))]
public class Player : MonoBehaviour
{   
 
    PlayerInputActions _input;
    private Vector2 _moveInput;
    private bool _jumpPressed;
    PersonMovement _movement;
    Camera _mainCam;
    [SerializeField]
    private PlayerAnimations _anims;
    [SerializeField]
    private CharacterData _character;

    void Start()
    {
        _movement = GetComponent<PersonMovement>();
        _input = new();
        _input.Enable();
        _mainCam = Camera.main;

        _input.Movement.Jump.performed += JumpPressed;
        _input.Movement.Item.performed += ItemPressed;
    }

    void Update()
    {
        HandlePlayerInput();
    }

    // Input Event
    private void JumpPressed(InputAction.CallbackContext ctx)
    {
        _jumpPressed = true;
    }

    // Input Event
    void ItemPressed(InputAction.CallbackContext ctx)
    {

    }

    void HandlePlayerInput()
    {
        _moveInput = _input.Movement.Move.ReadValue<Vector2>();
        
        if (_movement == null)
            _movement = GetComponent<PersonMovement>();
            
        var playerInput = Vector2.ClampMagnitude(_moveInput, 1);

        Vector3 camRight = _mainCam.transform.right;
        Vector3 camForward = _mainCam.transform.forward;
        camRight.y = camForward.y = 0;
        
        camRight.Normalize();
        camForward.Normalize();

        Vector3 xComponent = playerInput.x * camRight;
        Vector3 yComponent = playerInput.y * camForward;

        _movement.SetDesiredDirection(xComponent + yComponent);
        _movement.SetJumpRequested(_jumpPressed);

        _jumpPressed = false;
    }
}
