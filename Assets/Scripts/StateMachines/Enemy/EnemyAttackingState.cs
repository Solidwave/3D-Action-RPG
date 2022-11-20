using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingStae : EnemyBaseState
{

    private readonly int AttackHash = Animator.StringToHash("Attack");


    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public EnemyAttackingStae(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);

        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}
