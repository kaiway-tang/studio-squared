using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : BossEntity
{

    [SerializeField] public TB_Env bossEnv;

    [SerializeField] float farRange = 0;

    public TB_MoveState moveState { get; private set; }
    public TB_DashState dashState { get; private set; }

    public TB_SpawnPillarsState pillarState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_AttackState dashStateData;
    [SerializeField] private D_AttackState spawnPillarsStateData;

    [SerializeField] private float timeBeforeFirstAttack = 3f;
    protected override void Start()
    {

        base.Start();
        moveState = new TB_MoveState(this, stateMachine, moveStateData, this);
        dashState = new TB_DashState(this, stateMachine, dashStateData, this);
        pillarState = new TB_SpawnPillarsState(this, stateMachine, dashStateData, this); //TODO: FIX DATA LATER
        stateMachine.Initialize(moveState);
        stateDict = new StateDict();

        stateDict.AddStateList(new List<State> { pillarState, dashState }, "far1");

        StartCountdownA(timeBeforeFirstAttack);

    }


    public State AttackDecider(float locationDiff)
    {
        
        if (Mathf.Abs(locationDiff) >= farRange)
        {
            return stateDict.SelectRandState("far1");
        }
        return moveState;
    }


}
