using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack Attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackId) : base(stateMachine)
    {
        Attack = stateMachine.Attacks[attackId];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(Attack.AnimationName,0.5f);
    }

    public override void Exit()
    {
    }

    public override void Tick(float deltaTime)
    {
    }
}
