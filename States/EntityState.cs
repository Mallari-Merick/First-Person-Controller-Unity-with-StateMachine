using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;
    protected Rigidbody rb;
    protected StateChecks stateChecks;

    protected float stateTimer;
    public EntityState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName, StateChecks stateChecks)
    {
        this.player = player;
        this.rb = rb;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
        this.stateChecks = stateChecks;
    }
    public virtual void Enter()
    {
        stateTimer = 0f;
    }
    public virtual void Update()
    {
        stateTimer += Time.deltaTime;
        Debug.Log("I Update in:" + stateName);
    }
    public virtual void FixedUpdate()
    {
        
    }
    public virtual void Exit()
    {
        
    }
}