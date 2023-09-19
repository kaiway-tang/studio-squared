using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScythe : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] Transform mask;
    Transform trfm;
    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleTracking();
    }

    Vector3 vect3;
    bool facingLeft;
    void HandleTracking()
    {
        trfm.position += (targetPos.position - trfm.position) * .1f;
        if (facingLeft)
        {
            if (targetPos.position.x > trfm.position.x)
            {
                FlipFacing();
                facingLeft = false;
            }
        }
        else
        {
            if (targetPos.position.x < trfm.position.x)
            {
                FlipFacing();
                facingLeft = true;
            }
        }
    }

    void FlipFacing()
    {
        vect3 = trfm.localScale;
        vect3.x *= -1;
        trfm.localScale = vect3;
    }

    public void Dematerialize()
    {

    }

    public void Materialize()
    {

    }
}
