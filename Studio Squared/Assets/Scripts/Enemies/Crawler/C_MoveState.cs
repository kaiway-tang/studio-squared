using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_MoveState : MoveState
{
    private Crawler enemy;
    private float flipCooldown;

    public C_MoveState(StateEntity entity, FiniteStateMachine stateMachine, D_MoveState stateData, Crawler enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
        flipCooldown = 0;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if ((!isTouchingGround || isTouchingWall) && flipCooldown <= 0)
        {
            entity.Flip();
            flipCooldown = 0.25f;
            //maybe make this a "flipstate", but that's probably not needed
        }
        flipCooldown -= Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.AddForwardVelocity(stateData.moveSpeed, stateData.maxSpeed);
    }


}
