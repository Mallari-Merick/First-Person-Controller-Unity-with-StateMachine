using UnityEngine;

public class Player_FallState : Player_AirState
{
    public Player_FallState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName, StateChecks stateChecks) : base(player, rb, stateMachine, stateName, stateChecks)
    {
    }
    private Vector3 airMomentum;
    public override void Enter()
    {
        base.Enter();
        // Capture horizontal momentum when entering air state
        airMomentum = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
    }
    public override void Update()
    {
        base.Update();
        if (stateChecks.IsGrounded())
        {
            // To Move State
            if(player.moveVector.sqrMagnitude > 0.01f)
                stateMachine.ChangeState(player.moveState);
            // To Idle State
            if(player.moveVector == Vector2.zero)
                stateMachine.ChangeState(player.idleState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        AirMove();
    }
    void AirMove()
    {
        Vector3 inputDirection = player.GetCameraRelativeMovement(player.moveVector);

        // Blend between the momentum and new input
        float airControlInfluence = player.airControlStrength;

        Vector3 targetVelocity = Vector3.Lerp(
            airMomentum,
            inputDirection * player.moveSpeed * player.inAirSpeedMultiplier,
            airControlInfluence
        );

        // Apply the horizontal velocity
        rb.linearVelocity = new Vector3(
            targetVelocity.x,
            rb.linearVelocity.y,
            targetVelocity.z
        );

        airMomentum = Vector3.Lerp(airMomentum, targetVelocity, airControlInfluence * 0.5f);
    }
}