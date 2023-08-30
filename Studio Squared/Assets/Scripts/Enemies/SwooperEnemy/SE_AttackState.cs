using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_AttackState : AttackState
{
    SwooperEnemy enemy;
    Vector2 playerPos;

    private float baseSwoopTimer = 0.75f;
    private float swoopTimer;
    private float baseRiseTimer = 1f;
    private float riseTimer;
    private bool rise;
    public SE_AttackState(StateEntity entity, FiniteStateMachine stateMachine, D_AttackState stateData, SwooperEnemy enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        Debug.Log("Enter!~");
        playerPos = Player.GetPredictedPosition(0);
        swoopTimer = baseSwoopTimer;
        Debug.Log(swoopTimer);
        rise = false;
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        
        if(!rise)
        {
            swoopTimer -= Time.deltaTime;
            if(swoopTimer <= 0)
            {
                rise = true;
                riseTimer = baseRiseTimer;
            }
        }
        else
        {
            //Debug.Log("Rise");
            riseTimer -= Time.deltaTime;
            if(riseTimer <= 0)
            {
                enemy.stateMachine.ChangeState(enemy.moveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!rise)
        {
            var dif = playerPos - (Vector2)enemy.transform.position;
            //playerPos = Player.GetPredictedPosition(0);
            enemy.AddVelocity(3 * Time.deltaTime * dif, 3);
        }
        else
        {
            enemy.AddVelocity(Time.deltaTime * Vector2.up, 2);
        }

    }
}
