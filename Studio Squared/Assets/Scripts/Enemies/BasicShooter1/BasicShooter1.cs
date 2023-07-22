using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShooter1 : StateEntity
{
    public BS1_MoveState moveState { get; private set; }
    public BS1_AdvanceState advanceState { get; private set; }
    public BS1_RetreatState retreatState { get; private set; }
    public BS1_AttackState attackState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetected advanceStateData;
    [SerializeField] private D_PlayerDetected retreatStateData;
    [SerializeField] private D_AttackState attackStateData;

    protected override void Start()
    {
        base.Start();
        moveState = new BS1_MoveState(this, stateMachine, moveStateData, this);
        advanceState = new BS1_AdvanceState(this, stateMachine, advanceStateData, this);
        retreatState = new BS1_RetreatState(this, stateMachine, retreatStateData, this);
        attackState = new BS1_AttackState(this, stateMachine, attackStateData, this);
        Debug.Log(moveState);
        stateMachine.Initialize(moveState);
    }


}
