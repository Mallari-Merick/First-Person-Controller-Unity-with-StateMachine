using UnityEngine;

public abstract class Player_AirState : PlayerState
{
    float minJumpTime = 0.1f;
    public Player_AirState(Player player, StateMachine stateMachine, string stateName, Rigidbody rb, StateChecks stateChecks) : base(player, stateMachine, stateName, rb, stateChecks)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = 0;
    }
    public override void Update()
    {
        base.Update();
        if(stateTimer > minJumpTime) return;
        if(stateChecks.IsGrounded())
            stateMachine.ChangeState(player.idleState);
        if(inputActions.Player.Crouch.WasPressedThisDynamicUpdate())
            stateMachine.ChangeState(player.slamState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    void Move()
    {
        Vector3 getCameraRelativeMovemet = player.GetCameraRelativeMovement(player.moveVector);
        float moveSpeed = player.movementSpeed * player.airStrafeMultiplier;
        player.SetVelocityXZ(getCameraRelativeMovemet, moveSpeed, player.airAcceleration);
    }
}
