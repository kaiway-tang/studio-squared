using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningScythe : Attack
{
    [SerializeField] int spinRate;
    [SerializeField] float initVelocity, reverseAccl;
    [SerializeField] bool facingLeft;
    float velocity;
    Transform trfm;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        trfm = transform;
        gameObject.SetActive(false);
    }

    public void Init(Vector2 position, bool p_facingLeft)
    {
        trfm.position = position;
        velocity = p_facingLeft ? -initVelocity : initVelocity;
        facingLeft = p_facingLeft;
        Activate(p_facingLeft ? 1 : 0, 999, true);
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        trfm.Rotate(Vector3.forward * spinRate);
        trfm.position += Vector3.right * velocity;

        if (facingLeft)
        {
            if (velocity < initVelocity)
            {
                velocity += reverseAccl;
            }
            else
            {
                End();
            }
        }
        else
        {
            if (velocity > -initVelocity)
            {
                velocity -= reverseAccl;
            }
            else
            {
                End();
            }
        }
    }

    void End()
    {
        gameObject.SetActive(false);
    }


}
