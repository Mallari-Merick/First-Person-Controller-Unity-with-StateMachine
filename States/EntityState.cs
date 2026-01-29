using UnityEngine;

public abstract class EntityState 
{
    protected StateMachine stateMachine;
    protected string stateName;
    protected Rigidbody rb;
    protected StateChecks stateChecks;

    protected float stateTimer;
    public EntityState(StateMachine stateMachine, string stateName, Rigidbody rb, StateChecks stateChecks)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
        this.rb = rb;
        this.stateChecks = stateChecks;
    }
    public virtual void Enter()
    {
        
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void FixedUpdate()
    {
        // Debug.Log("I Fixed Update in: "+ stateName);
    }
    public virtual void Exit()
    {
        // Debug.Log("I Exit: "+ stateName);
    }
}
