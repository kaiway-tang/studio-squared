using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public class ReferenceState
    {
        int ID, priority;

        public ReferenceState(int pID, int pPriority)
        {
            ID = pID;
            priority = pPriority;
        }

        public int getID() { return ID; }
        public int getPriority() { return priority; }
    }
    public class ActiveState
    {
        public bool hasReference;
        public int duration;
        public ReferenceState referenceState;

        public ActiveState()
        {
            hasReference = false;
            duration = 0;
        }
        public ActiveState(ReferenceState pReferenceState)
        {
            referenceState = pReferenceState;
            hasReference = true;
        }

        public void SetReferenceState(ReferenceState pReferenceState)
        {
            referenceState = pReferenceState;
            hasReference = true;
        }
        public void SetReferenceState(ReferenceState pReferenceState, int pDuration)
        {
            referenceState = pReferenceState;
            duration = pDuration;
            hasReference = true;
        }

        public int getID()
        {
            if (hasReference) { return referenceState.getID(); }
            return -1;
        }
        public int getPriority()
        {
            if (hasReference) { return referenceState.getPriority(); }
            return -1;
        }
    }

    [SerializeField] Animator animator;
    //[SerializeField] int animatorState, firstEmptyQueSlot, activeAnimations, defaultAnimation;
    int firstEmptyQueSlot, activeAnimations;

    protected ActiveState currentState, defaultState;

    //[SerializeField] protected int[] animationQueID, animationQueDuration, animationPriority;
    protected ActiveState[] animationQue;

    protected void Start()
    {
        currentState = new ActiveState();
        defaultState = new ActiveState();

        animationQue = new ActiveState[] { new ActiveState(), new ActiveState(), new ActiveState() };
    }

    protected void FixedUpdate()
    {
        for (int i = 0; i < animationQue.Length; i++)
        {
            if (animationQue[i].duration > 0)
            {
                animationQue[i].duration--;

                if (animationQue[i].duration < 1)
                {
                    if (animationQue[i].getID() == currentState.getID()) { currentState.hasReference = false; }

                    animationQue[i].hasReference = false;
                    activeAnimations--;

                    ActivateNextState();
                }
            }
        }
    }

    public bool RequestAnimatorState(ReferenceState referenceState) //returns true if requested animation is set
    {
        if (currentState.getID() != referenceState.getID())
        {
            if (referenceState.getPriority() >= currentState.getPriority())
            {
                animator.SetInteger("State", referenceState.getID());
                currentState.SetReferenceState(referenceState);
                return true;
            }
            else
            {
                //defaultState.SetReferenceState(referenceState);
            }
        }
        return false;
    }

    public void QueAnimation(ReferenceState referenceState, int duration)
    {
        if (activeAnimations > 0)
        {
            firstEmptyQueSlot = -1;

            for (int i = 0; i < animationQue.Length; i++)
            {
                if (firstEmptyQueSlot == -1 && !animationQue[i].hasReference) //no empty que slot found && found empty que slot
                {
                    firstEmptyQueSlot = i;
                }
                if (animationQue[i].getID() == referenceState.getID())
                {
                    if (animationQue[i].duration < duration)
                    {
                        animationQue[i].duration = duration;
                    }
                    return;
                }
            }

            if (firstEmptyQueSlot == -1)
            {
                Debug.Log("OH NO ARRAY TOOOO SMALLLL");
                return;
            }
        }
        else
        {
            firstEmptyQueSlot = 0;
        }

        animationQue[firstEmptyQueSlot].SetReferenceState(referenceState, duration);
        RequestAnimatorState(referenceState);

        activeAnimations++;
    }

    public void DeQueAnimation(ReferenceState referenceState)
    {
        for (int i = 0; i < animationQue.Length; i++)
        {
            if (animationQue[i].getID() == referenceState.getID())
            {
                if (animationQue[i].getID() == currentState.getID()) { currentState.hasReference = false; }

                animationQue[i].hasReference = false;
                animationQue[i].duration = 0;
                activeAnimations--;

                ActivateNextState();

                return;
            }
        }
    }

    int indexGreatest;
    int IndexOfGreatestActivePriority()
    {
        indexGreatest = 0;
        for (int i = 1; i < animationQue.Length; i++)
        {
            if (animationQue[i].getPriority() > animationQue[indexGreatest].getPriority())
            {
                indexGreatest = i;
            }
        }

        return indexGreatest;
    }

    void ActivateNextState()
    {
        if (activeAnimations > 0)
        {
            RequestAnimatorState(animationQue[IndexOfGreatestActivePriority()].referenceState);
        }
        else
        {
            RequestAnimatorState(defaultState.referenceState);
        }
    }
}