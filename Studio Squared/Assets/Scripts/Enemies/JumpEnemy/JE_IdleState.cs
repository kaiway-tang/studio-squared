using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JE_IdleState : IdleState
{

    JumpEnemy enemy;
    float idleTimer;

    public JE_IdleState(StateEntity entity, FiniteStateMachine stateMachine, D_IdleState stateData, JumpEnemy enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        idleTimer = Random.Range(1.4f, 2.6f);
        //TODO: setup timerA to apply friction
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        idleTimer -= Time.deltaTime;
        if(idleTimer <= 0)
        {
            enemy.stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        enemy.ApplyXFriction(0.3f);
    }
}
