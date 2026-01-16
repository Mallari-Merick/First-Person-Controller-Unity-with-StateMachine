using UnityEngine;

public class Player_CrouchState : EntityState
{
    public Player_CrouchState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName) : base(player, rb, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
        // To Idle State
        if(!player.HandleCrouching() && player.moveVector == Vector2.zero)
            stateMachine.ChangeState(player.idleState);
        // To Move State
        if(!player.HandleCrouching() && player.moveVector.sqrMagnitude > 0.01)
            stateMachine.ChangeState(player.moveState);
        // To Jump state
        if(player.HandleJumping() && player.stateChecks.IsGrounded())
            stateMachine.ChangeState(player.jumpState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        CrouchMove();
    }

    private void CrouchMove()
    {
        Vector3 getCameraRelativeMovement = player.GetCameraRelativeMovement(player.moveVector);
        float crouchSpeed = player.moveSpeed * player.crouchSpeedMultiplier;
        player.SetVelocityXZ(getCameraRelativeMovement, crouchSpeed);
    }
}