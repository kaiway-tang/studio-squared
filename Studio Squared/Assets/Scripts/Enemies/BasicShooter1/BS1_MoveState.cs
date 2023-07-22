using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS1_MoveState : MoveState
{
    private BasicShooter1 enemy;
    private float flipCooldown;

    protected bool isPlayerInMaxAggroRange;
    protected bool isPlayerInMinAggroRange;

    public BS1_MoveState(StateEntity entity, FiniteStateMachine stateMachine, D_MoveState stateData, BasicShooter1 enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
        flipCooldown = 0;
    }

    public override void Enter()
    {
        base.Enter();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        if (isPlayerInMinAggroRange)
        {
            stateMachine.ChangeState(enemy.retreatState);
        }
        else if (isPlayerInMaxAggroRange)
        {
            stateMachine.ChangeState(enemy.advanceState);
        }
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
