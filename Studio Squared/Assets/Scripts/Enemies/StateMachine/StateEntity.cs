using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEntity : MobileEntity
{

    public FiniteStateMachine stateMachine;

    [SerializeField] protected D_StateEntity baseData;

    //detect player
    [SerializeField] private Transform playerCheck;

    private float timerA; //usually used for attacking

    protected virtual void Start(){
        base.Start();
        stateMachine = new FiniteStateMachine();
    }

    protected virtual void Update()
    {
        timerA -= Time.deltaTime;
        stateMachine.currentState.LogicUpdate();
        EntityLogicUpdate();
    }
    

    public virtual void EntityLogicUpdate()
    {
        //pass
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
        EntityPhysicsUpdate();
    }

    public virtual void EntityPhysicsUpdate()
    {
        //pass
    }

    public bool IsTouchingWall()
    {
        return terrainTriggers[1].isTouching > 0;
    }
 
    public bool IsTouchingGround()
    {
        return IsOnGround();
    }

    public void Flip()
    {
        FlipFacing();
    }

    public void AddForwardVelocity(float x, float y)
    {
        AddForwardXVelocity(x, y);
    }

    public virtual bool CheckPlayerInMaxAggroRange()
    {
        RaycastHit2D res = Physics2D.Raycast(playerCheck.position, transform.right * transform.localScale.x, baseData.maxAggroDistance);
        if (res)
        {
            Debug.Log(res.transform.name);
            if (res.transform.tag == "Player")
            {
                //Debug.Log("Saw Player");
                return true;
            }
        }
        return false;
    }

    public virtual Transform GetPlayerInMaxAggroRange()
    {
        RaycastHit2D res = Physics2D.Raycast(playerCheck.position, transform.right * transform.localScale.x, baseData.maxAggroDistance);
        if (res)
        {
            if (res.transform.tag == "Player")
            {
                return res.transform;
            }
        }
        return null;
    }


    public virtual bool CheckPlayerInMinAggroRange()
    {
        RaycastHit2D res = Physics2D.Raycast(playerCheck.position, transform.right * transform.localScale.x, baseData.minAggroDistance);
        if (res)
        {
            if (res.transform.tag == "Player")
            {
                //Debug.Log("Saw Player");
                return true;
            }
        }
        return false;
    }


    public virtual Transform GetPlayerInMinAggroRange()
    {
        RaycastHit2D res = Physics2D.Raycast(playerCheck.position, transform.right * transform.localScale.x, baseData.minAggroDistance);
        if (res)
        {
            if (res.transform.tag == "Player")
            {
                return res.transform;
            }
        }
        return null;
    }

    public virtual Transform GetPlayerInAttackRange()
    {
        RaycastHit2D res = Physics2D.Raycast(playerCheck.position, transform.right * transform.localScale.x, baseData.attackDistance);
        if (res)
        {
            if (res.transform.tag == "Player")
            {
                return res.transform;
            }
        }
        return null;
    }

    new public void ApplyXFriction(float amount)
    {
        base.ApplyXFriction(amount);
    }

    public virtual void startCountdownA(float startValue)
    {
        this.timerA = startValue;
    }

    public virtual float getCountdownA()
    {
        return this.timerA;
    }


    void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position, transform.right * baseData.minAggroDistance);
    }
}
