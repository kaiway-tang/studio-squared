using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE1_PlayerDetectedState : PlayerDetectedState
{

    BasicEnemy1 enemy;
    public BE1_PlayerDetectedState(StateEntity entity, FiniteStateMachine stateMachine, D_PlayerDetected stateData, BasicEnemy1 enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        Debug.Log("Howdy player");
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        Debug.Log("Howdy player");
        base.LogicUpdate();
        if (!playerInMaxAggroRange)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
