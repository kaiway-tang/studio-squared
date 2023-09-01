using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float top, bottom, speed, destination;
    [SerializeField] bool inMovement;

    public enum Use
    {
        Bottom = 0,
        Top = 1,
        Move = 2,
        Parenter = 3
    }

    Transform trfm;
    void Start()
    {
        trfm = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inMovement)
        {
            trfm.position += Vector3.up * Movement();
        }
    }

    float Movement()
    {
        if (destination > trfm.position.y + speed)
        {
            return speed;
        }
        else if (destination < trfm.position.y - speed)
        {
            return -speed;
        }

        inMovement = false;
        return 0;
    }

    public void Trigger(Use use)
    {
        if (use == Use.Bottom)
        {
            destination = bottom;
        }
        else if (use == Use.Top)
        {
            destination = top;
        }
        else if (use == Use.Move)
        {
            if (Mathf.Abs(trfm.position.y - top) < speed)
            {
                destination = bottom;
            }
            else if (Mathf.Abs(trfm.position.y - bottom) < speed)
            {
                destination = top;
            }
        }

        inMovement = true;
    }
}
