using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_MoveState : MoveState
{

    private float flipCooldown;
    private Transform playerInAttackRange;    
    private TestBoss enemy;
    private Vector2 playerPos;


    public TB_MoveState(StateEntity entity, FiniteStateMachine stateMachine, D_MoveState stateData, TestBoss enemy) : base(entity, stateMachine, stateData)
    {
        base.doTerrainChecks = true;
        this.enemy = enemy;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerInAttackRange = entity.GetPlayerInAttackRange();
        if (playerInAttackRange)
        {
            //stateMachine.ChangeState(enemy.playerDetectedState);
        }
        /*
        if ((!isTouchingGround || isTouchingWall) && flipCooldown <= 0)
        {
            entity.Flip();
            flipCooldown = 0.25f;
            //maybe make this a "flipstate", but that's probably not needed
        }
        */
        playerPos = Player.GetPredictedPosition(0);
        if(flipCooldown <= 0)
        {
            if (playerPos.x < enemy.transform.position.x)
            {
                entity.SetFacing(true); //left = true
            }
            else if(playerPos.x > enemy.transform.position.x)
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
