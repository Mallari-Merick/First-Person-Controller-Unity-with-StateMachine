using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName, Rigidbody rb, StateChecks stateChecks) : base(player, stateMachine, stateName, rb, stateChecks)
    {
    }

    public override void Update()
    {
        base.Update();
        if(player.moveVector == Vector2.zero)
            stateMachine.ChangeState(player.idleState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    private void Move()
    {
        Vector3 cameraRelativeMovement = player.GetCameraRelativeMovement(player.moveVector);
        float movementSpeed = player.movementSpeed;
        player.SetVelocityXZ(cameraRelativeMovement, movementSpeed, player.groundAcceleration);
    }
}
