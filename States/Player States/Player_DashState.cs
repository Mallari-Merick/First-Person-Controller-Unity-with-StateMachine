using UnityEngine;

public class Player_DashState : PlayerState
{
    Vector3 dashDirection;
    Vector3 dashStartDirection;
    public Player_DashState(Player player, StateMachine stateMachine, string stateName, Rigidbody rb, StateChecks stateChecks) : base(player, stateMachine, stateName, rb, stateChecks)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration; //To compare to Dash Duration
        dashStartDirection = player.transform.position;

        if(player.moveVector.sqrMagnitude > 0.01f)
            dashDirection = player.GetCameraRelativeMovement(player.moveVector).normalized;
        else //No dash Direction so we Dash forward
        {
            dashDirection = player.cameraTransform.forward;
            dashDirection.y =0;
            dashDirection.Normalize();
        }
        rb.linearVelocity = dashDirection * player.dashSpeed;
    }
    public override void Update()
    {
        base.Update();
        float distanceTraveled = Vector3.Distance(dashStartDirection, player.transform.position);

        if(stateTimer < 0 || distanceTraveled >= player.dashLength)
        {
            if(stateChecks.IsGrounded())
            {
                if(player.moveVector.sqrMagnitude > 0.01f)
                    stateMachine.ChangeState(player.moveState);
                else
                    stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        HandleDash();
    }

    void HandleDash()
    {
        Vector3 currentHorizontal = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        float speedLimitSquared = player.movementSpeed * player.movementSpeed;
        if (currentHorizontal.sqrMagnitude > speedLimitSquared)
            rb.linearVelocity = dashDirection * player.dashSpeed;
    }
}
