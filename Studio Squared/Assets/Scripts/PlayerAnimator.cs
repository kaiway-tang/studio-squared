using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : AnimationController
{
    public ReferenceState
        Idle = new ReferenceState(0, 1),
        Run = new ReferenceState(1, 1),
        Jump = new ReferenceState(2, 1),
        Fall = new ReferenceState(3, 1),
        Dash = new ReferenceState(4, 50),
        Roll = new ReferenceState(5, 20),
        Attack1 = new ReferenceState(6, 100),
        Attack2 = new ReferenceState(7, 105),
        Cast = new ReferenceState(9, 110),
        Slide = new ReferenceState(8, 50),
        Death = new ReferenceState(10, 500);

    // Start is called before the first frame update
    new void Start()
    {
        currentState = new ActiveState(Idle);
        defaultState = new ActiveState(Idle);

        animationQue = new ActiveState[] { new ActiveState(), new ActiveState(), new ActiveState(), new ActiveState() };
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
