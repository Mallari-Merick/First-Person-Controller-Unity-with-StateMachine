using UnityEngine;

public class Player_SlamState : Player_AirState
{
    public Player_SlamState(Player player, StateMachine stateMachine, string stateName, Rigidbody rb, StateChecks stateChecks) : base(player, stateMachine, stateName, rb, stateChecks)
    {
    }
    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector3(0, -player.slamSpeed, 0);
    }
}
