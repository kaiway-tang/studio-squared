using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    public MoveState(StateEntity entity, FiniteStateMachine stateMachine, D_MoveState stateData) : base(entity, stateMachine)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
    }

}
