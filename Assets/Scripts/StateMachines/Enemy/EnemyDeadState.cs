using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);

        stateMachine.Weapon.gameObject.SetActive(false);

        GameObject.Destroy(stateMachine.Target);
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }


    public override void Tick(float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}
