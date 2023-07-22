using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS1_AdvanceState : PlayerDetectedState
{

    BasicShooter1 enemy;
    Transform player;
    bool isTouchingGround;
    bool isTouchingWall;
    bool isPlayerInMaxAggroRange;
    public BS1_AdvanceState(StateEntity entity, FiniteStateMachine stateMachine, D_PlayerDetected stateData, BasicShooter1 enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        //Debug.Log("Howdy player");
        base.Enter();
        this.player = null;
        this.isTouchingGround = enemy.IsTouchingGround();
        this.isTouchingWall = enemy.IsTouchingWall();
        this.isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

    //advance until player is in range or we don't see the player anymore
    public override void LogicUpdate()
    {
        //Debug.Log("Howdy player");
        base.LogicUpdate();
        player = enemy.GetPlayerInAttackRange();
        isTouchingGround = enemy.IsTouchingGround();
        isTouchingWall = enemy.IsTouchingWall();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();

        if (this.player) //not equal to null
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else if (!isTouchingGround || isTouchingWall || !isPlayerInMaxAggroRange)
        {
            enemy.stateMachine.ChangeState(enemy.moveState); //movestate handles the flipping
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.AddForwardVelocity(stateData.detectedMoveSpeed, stateData.maxDetectedMoveSpeed);
    }
}
