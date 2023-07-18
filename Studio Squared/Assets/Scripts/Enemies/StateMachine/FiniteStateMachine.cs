using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://youtu.be/UeNKl5HZI3o
public class FiniteStateMachine
{

    public State currentState{get; private set;}
  
    public void Initialize(State startingState){
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(State newState){
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }
}
