using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void FacePlayer()
    {

        if (stateMachine.Player == null)
        {
            return;
        }

        Vector3 faceDirection = stateMachine.Player.transform.position - stateMachine.Controller.transform.position;

        faceDirection.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(faceDirection);
    }

    protected bool IsInChangeRange()
    {
        if (stateMachine.Player.IsDead)
        {
            return false;
        }

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).magnitude;
        return playerDistanceSqr <= stateMachine.PlayerChasingRange;
    }

}
