using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_DashState : AttackState
{

    TestBoss enemy;
    bool isTouchingGround;
    bool isTouchingWall;

    //TODO: hardcoded for now
    float dashWindupInit = 0.5f;
    float dashWindup;
    float timeBeforeNextAttack = 2f;
    //end TODO


    //TEMP CODE
    Color originalColor;
    Color flashColor;

    bool dashing;

    public TB_DashState(StateEntity entity, FiniteStateMachine stateMachine, D_AttackState stateData, TestBoss enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;

        flashColor = new Color(0, 0, 255);
    }

    public override void Enter()
    {
        base.Enter();
        this.isTouchingGround = enemy.IsTouchingGround();
        this.isTouchingWall = enemy.IsTouchingWall();
        dashWindup = dashWindupInit;
        dashing = false;

        //TEMP CODE
        originalColor = enemy.GetComponent<SpriteRenderer>().color;
        enemy.GetComponent<SpriteRenderer>().color = flashColor;
        //END TEMP
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (dashing)
        {
            entity.AddForwardVelocity(5, 30); //TODO: hardcoded for now
        }

    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();
        isTouchingGround = enemy.IsTouchingGround();
        isTouchingWall = enemy.IsTouchingWall();
        if (dashWindup >= 0)
        {
            dashWindup -= Time.deltaTime;
            if(dashWindup <= 0)
            {
                dashing = true;
                //TEMP CODE
                enemy.GetComponent<SpriteRenderer>().color = originalColor;
                //END TEMP
            }
        }
        if (!isTouchingGround || isTouchingWall)
        {
            enemy.StartCountdownA(timeBeforeNextAttack);
            enemy.stateMachine.ChangeState(enemy.moveState); //movestate handles the flipping
        }
        //TODO: probably detect if hit player
    }

}
