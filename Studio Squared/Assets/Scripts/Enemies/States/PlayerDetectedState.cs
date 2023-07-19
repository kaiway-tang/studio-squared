using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;
    protected bool playerInMinAggroRange;
    protected bool playerInMaxAggroRange;
    
    public PlayerDetectedState(StateEntity entity, FiniteStateMachine stateMachine, D_PlayerDetected stateData) : base(entity, stateMachine)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        playerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        playerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
