using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JE_MoveState : MoveState
{

    private JumpEnemy enemy;
    float leapDuration;
    bool hasLept;

    public JE_MoveState(StateEntity entity, FiniteStateMachine stateMachine, D_MoveState stateData, JumpEnemy enemy) : base(entity, stateMachine, stateData)
    {
        base.doTerrainChecks = false;
        leapDuration = 0.7f;
        this.enemy = enemy;

    }

    public override void Enter()
    {
        base.Enter();
        hasLept = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (hasLept)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Leap();
    }

    void Leap()
    {
        Debug.Log("Boing");
        Debug.Log((Player.GetPredictedPosition(leapDuration).x - enemy.transform.position.x) / leapDuration);
        Debug.Log(leapDuration * enemy.rb.gravityScale * 5);

        enemy.SetXVelocity((Player.GetPredictedPosition(leapDuration).x - enemy.transform.position.x) / leapDuration);
        enemy.SetYVelocity(leapDuration * enemy.rb.gravityScale * 5);
        hasLept = true;
    }

}
