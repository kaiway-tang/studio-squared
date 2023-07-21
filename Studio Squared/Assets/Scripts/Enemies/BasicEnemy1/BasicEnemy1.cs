using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy1 : StateEntity
{
    public BE1_MoveState moveState { get; private set; }
    public BE1_PlayerDetectedState playerDetectedState { get; private set; }

    public BE1_AttackState attackState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedData;
    [SerializeField] private D_AttackState attackStateData;

    protected override void Start()
    {
        base.Start();
        moveState = new BE1_MoveState(this, stateMachine, moveStateData, this);
        playerDetectedState = new BE1_PlayerDetectedState(this, stateMachine, playerDetectedData, this);
        attackState = new BE1_AttackState(this, stateMachine, attackStateData, this);
        Debug.Log(moveState);
        stateMachine.Initialize(moveState);
    }


}
