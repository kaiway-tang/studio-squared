using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : StateEntity
{
    public C_MoveState moveState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;


    protected override void Start()
    {
        base.Start();
        moveState = new C_MoveState(this, stateMachine, moveStateData, this);
        stateMachine.Initialize(moveState);
    }
}
