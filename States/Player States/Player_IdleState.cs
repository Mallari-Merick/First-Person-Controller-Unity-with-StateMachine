using UnityEngine;

public class Player_IdleState : EntityState
{
    public Player_IdleState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName, StateChecks stateChecks) : base(player, rb, stateMachine, stateName, stateChecks)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector3.zero, 0);
    }
    public override void Update()
    {
        base.Update();
        // To move state
        if(player.moveVector.sqrMagnitude > 0.01f)
            stateMachine.ChangeState(player.moveState);
        // To Jump State
        if(player.HandleJumping() && stateChecks.IsGrounded())
            stateMachine.ChangeState(player.jumpState);
        // To Crouch State
        if(stateChecks.IsCrouching(player.inputActions))
            stateMachine.ChangeState(player.crouchState);
    }
}