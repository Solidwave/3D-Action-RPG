using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent+= OnTarget;

        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
         if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed , deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        CalculateRotation(movement, deltaTime);
    }

    private void CalculateRotation(Vector3  movement, float deltaTime)
    {
         stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent-= OnTarget;

    }

    public void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget())
        {
            return;
        }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 mainCameraForward, mainCameraRight;
        mainCameraForward = stateMachine.MainCameraTransform.transform.forward.normalized;

        mainCameraForward.y = 0;

        mainCameraRight = stateMachine.MainCameraTransform.transform.right.normalized;

        mainCameraRight.y = 0;

        return mainCameraForward * stateMachine.InputReader.MovementValue.y + mainCameraRight * stateMachine.InputReader.MovementValue.x;
    }
}
