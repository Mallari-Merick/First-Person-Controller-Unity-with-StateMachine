using UnityEngine;

public class Player_IdleState : EntityState
{
    public Player_IdleState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName) : base(player, rb, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityXZ(Vector3.zero, 0);
    }
    public override void Update()
    {
        base.Update();
        // To Crouch State
        if(player.HandleCrouching())
            stateMachine.ChangeState(player.crouchState);
        // To Move state
        if(player.moveVector.sqrMagnitude > 0.01f)
            stateMachine.ChangeState(player.moveState);
        // To Jump state
        if(player.HandleJumping() && player.stateChecks.IsGrounded())
            stateMachine.ChangeState(player.jumpState);
    }
}