using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_MoveState : MoveState
{

    private float flipCooldown;
    private Transform playerInAttackRange;    
    private TestBoss enemy;
    private float locationDiff;
    private Vector2 playerPos;

    float attackTimer;

    public TB_MoveState(StateEntity entity, FiniteStateMachine stateMachine, D_MoveState stateData, TestBoss enemy) : base(entity, stateMachine, stateData)
    {
        base.doTerrainChecks = true;
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        /*
        playerInAttackRange = entity.GetPlayerInAttackRange();
        if (playerInAttackRange)
        {
            //stateMachine.ChangeState(enemy.playerDetectedState);
        }*/

        playerPos = Player.GetPredictedPosition(0);
        locationDiff = enemy.transform.position.x - playerPos.x; //negative if player to RIGHT of the boss

        if (enemy.GetCountdownA() <= 0)
        {
            enemy.stateMachine.ChangeState(enemy.AttackDecider(locationDiff));
        }


        if (flipCooldown <= 0)
        {
            //don't need to check for wall because just always face player (who should not be clipped behind the wall anyway)
            if (locationDiff > 0)
            {
                entity.SetFacing(true); //left = true
            }
            else
            {
                entity.SetFacing(false);
            }
        }
        flipCooldown -= Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.AddForwardVelocity(stateData.moveSpeed, stateData.maxSpeed);
    }



}
