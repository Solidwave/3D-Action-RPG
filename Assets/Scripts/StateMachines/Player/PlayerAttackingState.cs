using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;
    private Attack Attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        Attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(Attack.AnimationName, Attack.TransitionDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {   
            if (normalizedTime > Attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        } else {
            if (stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            } else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }

        previousFrameTime = normalizedTime;
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (Attack.ComboStateIndex == -1) { return;}
        if (normalizedTime < Attack.ComboAttackTime) { return;}
        
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, Attack.ComboStateIndex));
    }


    private void TryApplyForce()
    {
        if (alreadyAppliedForce)
        {
            return;
        }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * Attack.Force);

        alreadyAppliedForce = true;
    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo =  stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        } else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        } else
        {
            return 0f;
        }
    }
}
