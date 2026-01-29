using UnityEngine;

public class Player_JumpState : Player_AirState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string stateName, Rigidbody rb, StateChecks stateChecks) : base(player, stateMachine, stateName, rb, stateChecks)
    {
    }
    float jumpKickForce = 2f;
    public override void Enter()
    {
        base.Enter();
        Vector3 jumpDirection = player.GetCameraRelativeMovement(player.moveVector);
        Vector3 jumpBoost = jumpDirection * jumpKickForce;
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x + jumpBoost.x, 
            player.jumpHeight, 
            rb.linearVelocity.z + jumpBoost.z
        );
    }

    public override void Update()
    {
        base.Update();
        // If y velocity goes down, character is falling. transfer to fall state
        if(rb.linearVelocity.y < 0)
            stateMachine.ChangeState(player.fallState);
    }
}
