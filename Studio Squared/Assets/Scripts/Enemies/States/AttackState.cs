using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected D_AttackState stateData;

    protected Transform player;

    public AttackState(StateEntity entity, FiniteStateMachine stateMachine, D_AttackState stateData) : base(entity, stateMachine)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        player = entity.GetPlayerInAttackRange(); //get reference to player if possible
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player = entity.GetPlayerInAttackRange();
    }
}
