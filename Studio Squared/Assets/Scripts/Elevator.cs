using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float top, bottom, speed, destination;
    [SerializeField] bool inMovement;
    [SerializeField] bool epic;

    [SerializeField] int rideTrauma, startTrauma;
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
            if (epic) { CameraController.SetTrauma(rideTrauma); }
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

    bool epicInvokeInProcess;
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
        else if (use == Use.Move && !inMovement)
        {
            if (epic)
            {
                CameraController.SetTrauma(startTrauma);
                if (!epicInvokeInProcess)
                {
                    epicInvokeInProcess = true;
                    Invoke("Move", .6f);
                }
            }
            else
            {
                Move();
            }
        }

        if (!epic) { inMovement = true; }
    }

    void Move()
    {
        CameraController.SetTrauma(startTrauma);
        if (Mathf.Abs(trfm.position.y - top) < Mathf.Abs(trfm.position.y - bottom))
        {
            destination = bottom;
        }
        else
        {
            destination = top;
        }

        epicInvokeInProcess = false;
        inMovement = true;
    }
}
