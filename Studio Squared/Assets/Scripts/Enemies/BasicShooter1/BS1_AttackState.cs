using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS1_AttackState : AttackState
{

    BasicShooter1 enemy;

    public BS1_AttackState(StateEntity entity, FiniteStateMachine stateMachine, D_AttackState stateData, BasicShooter1 enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!player)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
        else if (enemy.GetCountdownA() <= 0)
        {
            Debug.Log("Shoot!");
            enemy.StartCountdownA(stateData.cooldown); //done inside entity to prevent "attack cancelling" if player moves out and back in range 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.ApplyXFriction(10); //friction
    }
}