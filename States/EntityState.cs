using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected Rigidbody rb;
    protected StateMachine stateMachine;
    protected string stateName;

    protected float stateTimer;
    public EntityState(Player player, Rigidbody rb, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.rb = rb;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }
    public virtual void Enter()
    {
        // Debug.Log("I Enter: "+ stateName);
        stateTimer = 0f;
    }
    public virtual void Update()
    {
        // Debug.Log("I Update in: "+ stateName);
        stateTimer += Time.deltaTime;
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