using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_SpawnPillarsState : AttackState
{

    TestBoss enemy;
    bool isTouchingGround;
    bool isTouchingWall;


    //TODO: hardcoded
    float attackWindupInit = 0.5f;
    float attackWindup;
    float timeBeforeNextAttack = 2f;
    bool attacking;
    List<GameObject> pillars;
    float pillarSpeed = 6f;
    float holdDurationInit = 4f;
    float holdDuration;
    //end TODO



    public TB_SpawnPillarsState(StateEntity entity, FiniteStateMachine stateMachine, D_AttackState stateData, TestBoss enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;

    }

    public override void Enter()
    {
        base.Enter();
        attackWindup = attackWindupInit;
        holdDuration = holdDurationInit;
        attacking = false;
        pillars = new List<GameObject>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (attackWindup >= 0)
        {
            attackWindup -= Time.deltaTime;
            if (attackWindup <= 0)
            {
                var env = enemy.bossEnv;
                var pos = env.transform.position;
                var p1 = Object.Instantiate(env.pillarFab, new Vector2(pos.x + 5, pos.y - 7), env.transform.rotation);
                var p2 = Object.Instantiate(env.pillarFab, new Vector2(pos.x - 5, pos.y - 7), env.transform.rotation);
                pillars.Add(p1);
                pillars.Add(p2);
                attacking = true;
            }
        }
        holdDuration -= Time.deltaTime;
        if(holdDuration <= 0)
        {
            foreach (var p in pillars)
            {
                GameObject.Destroy(p);
            }
            enemy.StartCountdownA(timeBeforeNextAttack);
            enemy.stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (attacking)
        {
            foreach(var p in pillars)
            {
                p.transform.position = new Vector2(p.transform.position.x, p.transform.position.y + pillarSpeed * Time.deltaTime); //TODO: code in pillars instead, oops
            }
        }
    }

}
