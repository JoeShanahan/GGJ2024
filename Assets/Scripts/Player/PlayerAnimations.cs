using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public bool IsRunning = false;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private PersonMovement _playerMove;
    [SerializeField] private AnimationCurve _runMapping;
    [SerializeField] private float _runSpeedMultiplier = 1f;
    [SerializeField] private float _animationSpeedMul = 1f;
    [SerializeField] private float _skidSpeedThreshold = 1f;
    [SerializeField] private ParticleController _skidParticles;
    private Animator _anim;
    private bool _stopped;

    public event Action OnStunFinish;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SyncVars();   
    }

    void SyncVars()
    {
        float vel = _rigidBody.velocity.magnitude;

        //_anim.speed = AnimationPlaybackSpeed;
        _anim.SetFloat("moveSpeed", Mathf.Max(vel * _runSpeedMultiplier, 0.1f));
        _anim.SetBool("isRunning", _playerMove.IsGrounded && vel > 0.1f);
        _anim.SetBool("isGrounded", _playerMove.IsGrounded);
        _anim.SetFloat("runAnimation", _runMapping.Evaluate(_rigidBody.velocity.magnitude));

        if (_stopped == false)
            _anim.speed = _animationSpeedMul;
    }

    private void PullTrigger(string triggerName)
    {
        _anim.ResetTrigger(triggerName);
        _anim.SetTrigger(triggerName);  
    }

    public void DoJump() => PullTrigger("jumpTrigger");
}
