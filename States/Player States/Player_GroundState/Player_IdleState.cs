using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName, Rigidbody rb, StateChecks stateChecks) : base(player, stateMachine, stateName, rb, stateChecks)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if(player.moveVector.sqrMagnitude >= 0.01f)
            stateMachine.ChangeState(player.moveState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.SetVelocityXZ(Vector3.zero, 0,player.groundAcceleration);
    }
}
