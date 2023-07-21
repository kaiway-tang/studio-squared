using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE1_PlayerDetectedState : PlayerDetectedState
{

    BasicEnemy1 enemy;
    Transform player;
    bool isTouchingGround;
    bool isTouchingWall;
    public BE1_PlayerDetectedState(StateEntity entity, FiniteStateMachine stateMachine, D_PlayerDetected stateData, BasicEnemy1 enemy) : base(entity, stateMachine, stateData)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    //once we see the player we charge - no update to find player again needed except for attack range, we charge until we hit something or the player
    public override void LogicUpdate()
    {
        //Debug.Log("Howdy player");
        base.LogicUpdate();
        this.player = enemy.GetPlayerInAttackRange();
        this.isTouchingGround = enemy.IsTouchingGround();
        this.isTouchingWall = enemy.IsTouchingWall();
        if (this.player) //not equal to null
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else if(!isTouchingGround || isTouchingWall)
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
