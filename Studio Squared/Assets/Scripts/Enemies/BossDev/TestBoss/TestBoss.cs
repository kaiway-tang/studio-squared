using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : BossEntity
{

    public TB_MoveState moveState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;

    protected override void Start()
    {
        base.Start();
        moveState = new TB_MoveState(this, stateMachine, moveStateData, this);
        stateMachine.Initialize(moveState);
    }

}
