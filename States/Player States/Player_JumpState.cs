using UnityEngine;

public class Player_JumpState : EntityState
{
    public Player_JumpState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName, StateChecks stateChecks) : base(player, rb, stateMachine, stateName, stateChecks)
    {
    }
    float jumpBuffer = 0.1f;

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, player.jumpHeight, rb.linearVelocity.y);
    }
    public override void Update()
    {
        base.Update();
        if(stateTimer < jumpBuffer) return;

        if (stateChecks.IsGrounded())
        {
            if(player.moveVector.sqrMagnitude > 0.01f)
                stateMachine.ChangeState(player.moveState);
            if(player.moveVector == Vector2.zero)
                stateMachine.ChangeState(player.idleState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    private void Move()
    {
        Vector3 cameraRelativeMovement = player.GetCameraRelativeMovement(player.moveVector);
        float inAirSpeed = player.moveSpeed * player.inAirSpeedMultiplier;
        player.SetVelocity(cameraRelativeMovement, inAirSpeed);
    }
}