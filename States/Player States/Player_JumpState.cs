using UnityEngine;

public class Player_JumpState : EntityState
{
    float minJumpTime = 0.1f;
    public Player_JumpState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName) : base(player, rb, stateMachine, stateName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, player.jumpHeight, rb.linearVelocity.z);
    }
    public override void Update()
    {
        base.Update();
        if(stateTimer < minJumpTime) return;

        // Check if we're falling
        if(player.stateChecks.IsGrounded() && rb.linearVelocity.y <= 0.1f)
        {
            if(player.moveVector == Vector2.zero)
                stateMachine.ChangeState(player.idleState);
            // To Move State
            if(player.moveVector.sqrMagnitude > 0.01f)
                stateMachine.ChangeState(player.moveState);
        }
        // To Idle State
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }
    private void Move()
    {
        Vector3 getCameraRelativeMovement = player.GetCameraRelativeMovement(player.moveVector);
        float inAirSpeed = player.moveSpeed * player.inAirSpeedMultiplier;
        player.SetVelocityXZ(getCameraRelativeMovement, inAirSpeed);
    }
}