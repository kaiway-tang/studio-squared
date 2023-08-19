using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
// This line should always be present at the top of scripts which use pathfinding
using Pathfinding;


//borrowed from https://arongranberg.com/astar/documentation/4_2_5_05bb896f/astaraics.html

public class SE_MoveState : MoveState
{
    private SwooperEnemy enemy;

    //pathfind vars from enemy
    public Transform targetPosition;
    private Seeker seeker;
    public Path path;

    //will add to data later
    public float speed = 2;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;
    public float repathRate = 0.5f;
    private float lastRepath = float.NegativeInfinity;
    public bool reachedEndOfPath;
    private Vector3 velocity;

    private float playerOrbitDistance = 2f;
    private Vector2 playerPos;
    private float locationDiff;

    private float orbitTimer;
    private float orbitTimerBase = 0.5f;

    //path?
    public AIPath aiPath;

    public SE_MoveState(StateEntity entity, FiniteStateMachine stateMachine, D_MoveState stateData, SwooperEnemy enemy, Transform targetPosition, Seeker seeker, AIPath aiPath) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
        this.targetPosition = targetPosition;
        this.seeker = seeker;
        this.aiPath = aiPath;

        velocity = new Vector3();
        orbitTimer = orbitTimerBase;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // Disable the AIs own movement code

        Vector3 nextPosition;
        Quaternion nextRotation;
        // Calculate how the AI wants to move
        aiPath.MovementUpdate(Time.deltaTime, out nextPosition, out nextRotation);
        // Modify nextPosition and nextRotation in any way you wish
        // Actually move the AI
        aiPath.FinalizeMovement(nextPosition, nextRotation);
        //velocity = nextPosition - enemy.transform.position;
        //enemy.AddVelocity(velocity, 3); //TODO: fix vel
        //enemy.ApplyXFriction(0.5f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        playerPos = Player.GetPredictedPosition(0);
        locationDiff = Vector2.Distance(enemy.transform.position, playerPos);
        if(locationDiff <= playerOrbitDistance)
        {
            orbitTimer -= Time.deltaTime;
            if (orbitTimer <= 0)
            {
               // enemy.stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            orbitTimer = orbitTimerBase;
        }

    }
}

