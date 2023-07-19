using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;
    protected bool isTouchingWall;
    protected bool isTouchingGround;
    protected bool isPlayerInMaxAggroRange;
    protected bool isPlayerInMinAggroRange;

    public MoveState(StateEntity entity, FiniteStateMachine stateMachine, D_MoveState stateData) : base(entity, stateMachine)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        isTouchingGround = entity.IsTouchingGround();
        isTouchingWall = entity.IsTouchingWall();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isTouchingGround = entity.IsTouchingGround();
        isTouchingWall = entity.IsTouchingWall();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    }
}
