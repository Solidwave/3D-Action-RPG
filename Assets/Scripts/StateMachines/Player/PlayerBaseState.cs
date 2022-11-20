using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime );
    }

    protected void Move(float deltaTime)
    {
       Move(Vector3.zero, deltaTime);
    }

    protected void FaceTarget()
    {
        Target currentTarget = stateMachine.Targeter.CurrentTarget;

        if (currentTarget == null)
        {
            return;
        }

        Vector3 faceDirection = currentTarget.transform.position - stateMachine.Controller.transform.position;

        faceDirection.y = 0f;

        stateMachine.transform.rotation =  Quaternion.LookRotation(faceDirection);
    }

    protected void ReturnToLocomotion()
    {
        if (stateMachine.Targeter.CurrentTarget != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        } else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}