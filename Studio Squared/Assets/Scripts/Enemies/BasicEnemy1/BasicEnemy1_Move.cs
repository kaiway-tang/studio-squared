using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy1_Move : MoveState
{
    private BasicEnemy1 enemy;
    private float flipCooldown;

    public BasicEnemy1_Move(StateEntity entity, FiniteStateMachine stateMachine, D_MoveState stateData, BasicEnemy1 enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
        flipCooldown = 0;
    }

    public override void LogicUpdate()
    {
        if ((!entity.isTouchingGround || entity.isTouchingWall) && flipCooldown <= 0)
        {
            entity.Flip();
            flipCooldown = 0.25f;
            //maybe make this a "flipstate", but that's probably not needed
        }
        flipCooldown -= Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        entity.AddForwardVelocity(stateData.moveSpeed, stateData.maxSpeed);
    }


}
