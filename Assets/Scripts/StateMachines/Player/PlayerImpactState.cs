using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private const float CrossFadeDuration = 0.1f;
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private float duration = 1f;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick(float deltaTime)
    {
         Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0f)
        {
            ReturnToLocomotion();
        }
    }
   
}
