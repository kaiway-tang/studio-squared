using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy1 : StateEntity
{
    public BasicEnemy1_Move moveState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;

    protected override void Start()
    {
        base.Start();
        moveState = new BasicEnemy1_Move(this, stateMachine, moveStateData, this);
        stateMachine.Initialize(moveState);
    }
}
