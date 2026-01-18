using UnityEngine;

public class Player_CrouchState : EntityState
{
    public Player_CrouchState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName, StateChecks stateChecks) : base(player, rb, stateMachine, stateName, stateChecks)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.SetCrouchHeight(true);
    }
    public override void Exit()
    {
        base.Exit();
        player.SetCrouchHeight(false);
    }
    public override void Update()
    {
        base.Update();
        if (!stateChecks.IsCrouching(player.inputActions))
        {
            // To Move state
            if(player.moveVector.sqrMagnitude > 0.01f)
                stateMachine.ChangeState(player.moveState);
            // To Idle state
            if(player.moveVector == Vector2.zero)
                stateMachine.ChangeState(player.idleState);
            // To Jump State
            if(player.HandleJumping() && stateChecks.IsGrounded())
                stateMachine.ChangeState(player.jumpState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    private void Move()
    {
        Vector3 getCameraRelativeMovement = player.GetCameraRelativeMovement(player.moveVector);
        float moveSpeed = player.moveSpeed * player.crouchSpeedMultiplier;
        player.SetVelocity(getCameraRelativeMovement, moveSpeed);
    }
}