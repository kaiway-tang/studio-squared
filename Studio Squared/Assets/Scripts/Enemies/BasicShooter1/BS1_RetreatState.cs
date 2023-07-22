using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS1_RetreatState : PlayerDetectedState
{

    BasicShooter1 enemy;
    Transform player;
    bool isTouchingGround;
    bool isTouchingWall;
    bool isPlayerInMinAggroRange;
    public BS1_RetreatState(StateEntity entity, FiniteStateMachine stateMachine, D_PlayerDetected stateData, BasicShooter1 enemy) : base(entity, stateMachine, stateData)
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
        this.isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
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
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();

        if (!isTouchingGround || isTouchingWall || !isPlayerInMinAggroRange) 
        {
            if (this.player) //if we retreat ourselves into the corner, OR we get far enough from the player, attack
            {
                stateMachine.ChangeState(enemy.attackState);
            }
            else //if on the other hand the player fully left range
            {
                enemy.stateMachine.ChangeState(enemy.moveState); 
            }   
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.AddForwardVelocity(-stateData.detectedMoveSpeed, -stateData.maxDetectedMoveSpeed); //hopefully negative values are valid
    }
}
