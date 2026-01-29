using UnityEngine;

public abstract class Player_GroundedState : PlayerState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string stateName, Rigidbody rb, StateChecks stateChecks) : base(player, stateMachine, stateName, rb, stateChecks)
    {
    }

    public override void Update()
    {
        base.Update();
        if(inputActions.Player.Jump.WasPerformedThisFrame())
            stateMachine.ChangeState(player.jumpState);
    }
}
