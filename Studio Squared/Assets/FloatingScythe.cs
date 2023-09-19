using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScythe : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] SimpleAnimator dematerialize, materialize;
    [SerializeField] GameObject light2D;
    [SerializeField] float close, far;
    Transform trfm;
    int lightTimer;
    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lightTimer > 0)
        {
            lightTimer--;
            if (lightTimer == 0)
            {
                light2D.SetActive(false);
            }
        }
        HandleTracking();
    }

    Vector3 vect3;
    bool facingLeft;
    void HandleTracking()
    {
        if (Vector2.Distance(targetPos.position, trfm.position) < close)
        {
            trfm.up += (Vector3.up - trfm.up) * .1f;
        }
        else
        {
            trfm.up += ((targetPos.position - trfm.position) - trfm.up) * .1f;
        }
        trfm.position += (targetPos.position - trfm.position) * .1f;

        return;

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
        vect3.y *= -1;
        trfm.localScale = vect3;
    }

    public void Dematerialize()
    {
        lightTimer = 4;
        light2D.SetActive(true);
        dematerialize.Play();
    }

    public void Materialize()
    {
        lightTimer = 4;
        light2D.SetActive(true);
        trfm.position = targetPos.position;
        materialize.Play();
    }
}
