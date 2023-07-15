using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MobileEntity
{

    //state machine 
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }

    private State currentState;

    //wall & ground detection
    private bool wallDetected, groundDetected;
    [SerializeField] private float wallCheckDistance; //groundCheckDistance, 
    [SerializeField] private Transform groundCheck, wallCheck;

    //movement
    [SerializeField] private float movementSpeed;

        
    protected new void Start()
    {
        base.Start();
        currentState = State.Walking;
    }


    private void Update() {
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

    //Walking------------------------

    private void EnterWalkingState(){

    }

    private void UpdateWalkingState(){
        groundDetected = IsOnGround();
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance);//, whatIsGround);

        if(groundDetected || wallDetected){ //flip enemy
            SetFacing(!IsFacingLeft()); //right = false, so if isfacingleft we want to go to right (aka false), and if facing right we want to go left (aka true)
        }
        else{ //move enemy
            SetXVelocity(movementSpeed);
        }
        
        

    }

    private void ExitWalkingState(){

    }


    //knockback----------------------


    private void EnterKnockbackState(){

    }

    private void UpdateKnockbackState(){

    }

    private void ExitKnockbackState(){
        
    }

    //dead---------------------------

    
    private void EnterDeadState(){

    }

    private void UpdateDeadState(){

    }

    private void ExitDeadState(){
        
    }



    //functions-----------------------

    private void SwitchState(State state){
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
}
