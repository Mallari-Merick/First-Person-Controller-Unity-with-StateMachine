using UnityEngine;

public class Player_AirState : EntityState
{
    public Player_AirState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName, StateChecks stateChecks) : base(player, rb, stateMachine, stateName, stateChecks)
    {
    }
}