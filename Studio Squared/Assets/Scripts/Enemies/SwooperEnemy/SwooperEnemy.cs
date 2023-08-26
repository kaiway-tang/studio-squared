using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class SwooperEnemy : StateEntity
{

    public SE_MoveState moveState { get; private set; }
    public SE_AttackState attackState { get; private set; }
    //public BE1_PlayerDetectedState playerDetectedState { get; private set; }

    //public BE1_AttackState attackState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;
    //[SerializeField] private D_PlayerDetected playerDetectedData;
    [SerializeField] private D_AttackState attackStateData;



    //pathfind vars from enemy
    public Transform targetPosition;
    private Seeker seeker;
    public AIPath aiPath;

    protected override void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        moveState = new SE_MoveState(this, stateMachine, moveStateData, this, targetPosition, seeker, aiPath);
        //playerDetectedState = new BE1_PlayerDetectedState(this, stateMachine, playerDetectedData, this);
        //attackState = new SE_AttackState(this, stateMachine, attackStateData, this);
        stateMachine.Initialize(moveState);
        aiPath.canMove = false;
    }


}

