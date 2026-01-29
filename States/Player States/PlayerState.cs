using UnityEngine;

public class PlayerState : EntityState
{
    protected Player player;

    protected PlayerInputMap inputActions;

    public PlayerState(Player player, StateMachine stateMachine, string stateName, Rigidbody rb, StateChecks stateChecks) : base(stateMachine, stateName, rb, stateChecks)
    {
        this.player=player;
        inputActions = player.inputActions;
    }
    public override void Update()
    {
        base.Update();
        if(inputActions.Player.Dash.WasPressedThisFrame())
            stateMachine.ChangeState(player.dashState);
    }
}
