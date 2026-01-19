using UnityEngine;

public class Player_JumpState : Player_AirState
{
    public Player_JumpState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName, StateChecks stateChecks) : base(player, rb, stateMachine, stateName, stateChecks)
    {
    }
    float jumpBuffer = 0.1f;

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, player.jumpHeight, rb.linearVelocity.z);
    }
    public override void Update()
    {
        base.Update();
        if(stateTimer < jumpBuffer) return;

        // Transition to OnAir state when moving upward slows or reverses
        if(rb.linearVelocity.y <= 0f)
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }

        // Early landing during jump
        if (stateChecks.IsGrounded())
        {
            if(player.moveVector.sqrMagnitude > 0.01f)
                stateMachine.ChangeState(player.moveState);
            else
                stateMachine.ChangeState(player.idleState);
        }
    }
}