using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController
{
    private Animator _animator;
    private static int IdleKey = Animator.StringToHash("Idle");
    private static int SlipKey = Animator.StringToHash("Slip");

     private static int JumpKey = Animator.StringToHash("Jump");
    private static int HitKey = Animator.StringToHash("Hit");
    private static int MoveKey = Animator.StringToHash("Move");

    private int hitIndex;


    public CharacterAnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void PlayAnimation(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.Idle:
                PlayIdle();
                break;
            case AnimationType.Slip:
                PlaySlip();
                break;
            case AnimationType.Hit:
                PlayHit();
                break;
            case AnimationType.Move:
                PlayMove();
                break;
            case AnimationType.Jump:
                PlayJump();
                break;
        }
    }

    public void StopAnimation(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.Move:
                StopMove();
                break;
            case AnimationType.Hit:
                StopHit();
                break;
            case AnimationType.Slip:
                StopSlip();
                break;
            case AnimationType.Jump:
                StopJump();
                break;
        }
    }
    private void PlayIdle()
    {
        _animator.SetTrigger(IdleKey);
    }
    private void PlaySlip()
    {
        _animator.SetBool(SlipKey, true);
    }
    private void StopSlip()
    {
        _animator.SetBool(SlipKey, false);
    }

        private void PlayJump()
    {
        _animator.SetBool(JumpKey, true);
    }
    private void StopJump()
    {
        _animator.SetBool(JumpKey, false);
    }
        public bool GetJumpState()
    {
        return _animator.GetBool(JumpKey);
    }
    private void PlayHit()
    {
        _animator.SetInteger("HitIndex", Random.Range(0, 3));
        hitIndex = _animator.GetInteger("HitIndex");
        _animator.SetBool(HitKey, true);
    }
    public int GetHitIndex()
    {
        return hitIndex;
    }

    private void StopHit()
    {
        _animator.SetBool(HitKey, false);
    }
    public bool GetHitState()
    {
        return _animator.GetBool(HitKey);
    }
    public bool GetSlipState()
    {
        return _animator.GetBool(SlipKey);
    }
    private void PlayMove()
    {
        _animator.SetBool(MoveKey, true);
    }
    private void StopMove()
    {
        _animator.SetBool(MoveKey, false);
    }



}
