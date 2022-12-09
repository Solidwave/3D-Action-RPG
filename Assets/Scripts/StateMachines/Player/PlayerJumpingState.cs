using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");

    private Vector3 momentum;

    private const float CrossFadeDuration = 0.1f;
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

        stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetection;

        momentum = stateMachine.Controller.velocity;

        momentum.y = 0;

        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
    }

    public override void Exit()
    {
        stateMachine.LedgeDetector.OnLedgeDetect -= HandleLedgeDetection;

    }

    private void HandleLedgeDetection(Vector3 ledgeForward, Vector3 closestPoint)
    {
        stateMachine.SwitchState( new PlayerHangingState(stateMachine,ledgeForward, closestPoint));
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.Controller.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }

        FaceTarget();
    }
}
