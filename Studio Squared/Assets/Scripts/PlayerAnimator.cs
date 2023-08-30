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
        Dash = new ReferenceState(4, 50);

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
