using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);
        

        stateMachine.Weapon.gameObject.SetActive(false);
    }

  
    public override void Exit()
    {
    }


    public override void Tick(float deltaTime)
    {
    }

    private void OnEnable() {
        
    }

   
}
