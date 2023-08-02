using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MobileEntity
{

    //state machine 
    public enum State
    {
        Walking,
        Knockback,
        Dead
    }

    private State currentState;

    //wall & ground detection
    private bool wallDetected, groundDetected;
    //[SerializeField] private float wallCheckDistance; //groundCheckDistance, 
    //[SerializeField] private Transform groundCheck, wallCheck;

    //movement
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxSpeed;

        
    protected new void Start()
    {
        base.Start();
        currentState = State.Walking;
    }


    private void Update() {
        DoSwitch();
        DoUpdate();
    }

    //Walking------------------------

    protected virtual void EnterWalkingState(){

    }

    protected virtual void UpdateWalkingState(){
        groundDetected = IsOnGround();
        wallDetected = IsTouchingWall();//false;//Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance);//, whatIsGround);
        //Debug.Log(groundDetected);
        if(!groundDetected || wallDetected){ //flip enemy
            SetFacing(!IsFacingLeft()); //right = false, so if isfacingleft we want to go to right (aka false), and if facing right we want to go left (aka true)
        }
        else{ //move enemy
            //SetXVelocity(movementSpeed, maxSpeed);
            var speed = movementSpeed;
            AddForwardXVelocity(speed, maxSpeed);
        }
        
        

    }

    protected virtual void ExitWalkingState(){

    }


    //knockback----------------------


    protected virtual void EnterKnockbackState(){

    }

    protected virtual void UpdateKnockbackState(){

    }

    protected virtual void ExitKnockbackState(){
        
    }

    //dead---------------------------

    
    protected virtual void EnterDeadState(){

    }

    protected virtual void UpdateDeadState(){

    }

    protected virtual void ExitDeadState(){
        
    }



    //functions-----------------------

    protected virtual void DoSwitch(){
        switch(currentState){
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
            default:
                Debug.Log("State machine broke, called default");
                break;
        }
    }

    protected virtual void DoUpdate(){
        //Put anything else you need to be done on update here
    }


    protected virtual void SwitchState(State state){
        switch(currentState){
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
            default:
                Debug.Log("SwitchState exit broke, called default");
                break;
        }

        switch(state){
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
            default:
                Debug.Log("SwitchState start broke, called default");
                break;
        }
        currentState = state;
    }



    protected bool IsTouchingWall()
    {
        return terrainTriggers[1].isTouching > 0;
    }
}
