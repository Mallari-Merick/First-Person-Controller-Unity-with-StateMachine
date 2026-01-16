using UnityEngine;

public class StateMachine
{
    public EntityState currentState;
    public void Initialize(EntityState startState)
    {
        currentState = startState;
        currentState.Enter();
    }
    public void ChangeState(EntityState newState)
    {
        if(newState == null)
        {
            Debug.LogError("Trying to access a null state!");
            return;
        }
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    public void UpdateCurrentState()
    {
        currentState.Update();
    }
    public void FixedUpdateCurrentState()
    {
        currentState.FixedUpdate();
    }
}