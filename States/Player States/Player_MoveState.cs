using UnityEngine;

public class Player_MoveState : EntityState
{
    public Player_MoveState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName, StateChecks stateChecks) : base(player, rb, stateMachine, stateName, stateChecks)
    {
    }

    public override void Update()
    {
        base.Update();
        // To Idle State
        if(player.moveVector == Vector2.zero)
            stateMachine.ChangeState(player.idleState);
        // To Jump State
        if(player.HandleJumping() && stateChecks.IsGrounded())
            stateMachine.ChangeState(player.jumpState);
        // To Crouch State
        if(stateChecks.IsCrouching(player.inputActions))
            stateMachine.ChangeState(player.crouchState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    private void Move()
    {
        Vector3 cameraRelativeMovement = player.GetCameraRelativeMovement(player.moveVector);
        float moveSpeed = stateChecks.IsSprinting(player.inputActions)? player.sprintSpeed: player.moveSpeed;
        player.SetVelocity(cameraRelativeMovement, moveSpeed);
    }
}