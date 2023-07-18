using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEntity : MobileEntity
{

    public FiniteStateMachine stateMachine;


    //wall/ground
    public bool isTouchingGround;
    public bool isTouchingWall;


    protected virtual void Start(){
        base.Start();
        stateMachine = new FiniteStateMachine();
    }

    protected virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
        isTouchingGround = IsOnGround();
        isTouchingWall = IsTouchingWall();
    }

    protected bool IsTouchingWall()
    {
        return terrainTriggers[1].isTouching > 0;
    }

    public void Flip()
    {
        FlipFacing();
    }

    public void AddForwardVelocity(float x, float y)
    {
        AddForwardXVelocity(x, y);
    }

}
