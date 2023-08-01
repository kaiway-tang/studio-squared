using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : StateEntity
{



    public JE_MoveState moveState { get; private set; }
    public JE_IdleState idleState { get; private set; }
    //public JE_AttackState attackState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_AttackState attackStateData;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        base.Start();
        moveState = new JE_MoveState(this, stateMachine, moveStateData, this);
        idleState = new JE_IdleState(this, stateMachine, idleStateData, this);
        //attackState = new JE_AttackState(this, stateMachine, attackStateData, this);
        stateMachine.Initialize(moveState);
    }

}

    /*
     * 
    [SerializeField] float leapDuration;
    [SerializeField] EnemyDemoAttack attack;
    int attackCooldown, attackTimer;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        attackCooldown = Random.Range(70, 130);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { Leap(); }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attackCooldown > 0)
        {
            attackCooldown--;
        }
        else
        {
            Leap();
            attackCooldown = Random.Range(70, 130);
        }

        if (attackTimer > 0)
        {
            if (attackTimer == 1)
            {
                attack.Activate(-1, 4);
            }
            attackTimer--;
        }
        else
        {
            ApplyXFriction(.3f);
        }
    }

    void Leap()
    { 
        attackTimer = 35;
        SetXVelocity((Player.GetPredictedPosition(leapDuration).x - trfm.position.x)/leapDuration);
        SetYVelocity(leapDuration * rb.gravityScale * 5);
    }
    */

