using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;
    protected bool isTouchingWall;
    protected bool isTouchingGround;

    public bool doTerrainChecks = true;

    public MoveState(StateEntity entity, FiniteStateMachine stateMachine, D_MoveState stateData) : base(entity, stateMachine)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        if (doTerrainChecks)
        {
            isTouchingGround = entity.IsTouchingGround();
            isTouchingWall = entity.IsTouchingWall();
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (doTerrainChecks)
        {
            isTouchingGround = entity.IsTouchingGround();
            isTouchingWall = entity.IsTouchingWall();
        }

    }
}
