using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy1 : StateEntity
{
    public BasicEnemy1_Move moveState { get; private set; }
    public BE1_PlayerDetectedState playerDetectedState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedData;

    protected override void Start()
    {
        base.Start();
        moveState = new BasicEnemy1_Move(this, stateMachine, moveStateData, this);
        playerDetectedState = new BE1_PlayerDetectedState(this, stateMachine, playerDetectedData, this);
        stateMachine.Initialize(moveState);
    }
}
