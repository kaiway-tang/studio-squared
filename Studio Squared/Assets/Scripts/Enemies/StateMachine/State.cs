using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected StateEntity entity;
    
    protected float startTime;


    public State(StateEntity entity, FiniteStateMachine stateMachine){
        this.entity = entity;
        this.stateMachine = stateMachine;
    }

    //called on state enter
    public virtual void Enter(){
        startTime = Time.time;
    }

    //called on state exit
    public virtual void Exit(){

    }

    //called on Update()
    public virtual void LogicUpdate(){
        
    }

    //called on FixedUpdate()
    public virtual void PhysicsUpdate(){

    }



}
