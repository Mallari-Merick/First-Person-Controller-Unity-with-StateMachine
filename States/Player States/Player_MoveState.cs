using UnityEngine;

public class Player_MoveState : EntityState
{
    public Player_MoveState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName) : base(player, rb, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
        // To Crouch State
        if(player.HandleCrouching())
            stateMachine.ChangeState(player.crouchState);
        // To Idle State
        if(player.moveVector == Vector2.zero)
            stateMachine.ChangeState(player.idleState);
        // To Jump state
        if(player.HandleJumping() && player.stateChecks.IsGrounded())
            stateMachine.ChangeState(player.jumpState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    private void Move()
    {
        Vector3 getCameraRelativeMovement = player.GetCameraRelativeMovement(player.moveVector);
        player.SetVelocityXZ(getCameraRelativeMovement, player.moveSpeed);
    }
}