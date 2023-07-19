using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEntity : MobileEntity
{

    public FiniteStateMachine stateMachine;

    [SerializeField] protected D_StateEntity baseData;

    //detect player
    [SerializeField] private Transform playerCheck;

    protected virtual void Start(){
        base.Start();
        stateMachine = new FiniteStateMachine();
    }

    protected virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
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
        RaycastHit2D res = Physics2D.Raycast(playerCheck.position, transform.right, baseData.maxAggroDistance);
        if(res.transform.tag == "Player")
        {
            Debug.Log("Saw Player");
            return true;
        }
        return false;
    }

    public virtual bool CheckPlayerInMinAggroRange()
    {
        RaycastHit2D res = Physics2D.Raycast(playerCheck.position, transform.right, baseData.minAggroDistance);
        Debug.Log(res.transform.name);
        if (res.transform.tag == "Player")
        {
            Debug.Log("Saw Player");
            return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * baseData.minAggroDistance);
    }
}
