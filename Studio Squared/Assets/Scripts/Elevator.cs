using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float top, bottom, maxSpeed, accl, destination;
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

    [SerializeField] float effectiveSpeed;
    float Movement()
    {
        if (destination > trfm.position.y + maxSpeed)
        {
            if (effectiveSpeed < maxSpeed)
            {
                effectiveSpeed += accl;
            }
            return effectiveSpeed;
        }
        else if (destination < trfm.position.y - maxSpeed)
        {
            if (effectiveSpeed > -maxSpeed)
            {
                effectiveSpeed -= accl;
            }
            return effectiveSpeed;
        }

        CameraController.SetTrauma(startTrauma);
        effectiveSpeed = 0;
        inMovement = false;
        return GetCloserDestination() - trfm.position.y;
    }

    bool epicInvokeInProcess;
    public void Trigger(Use use)
    {
        if (use == Use.Bottom)
        {
            if (Mathf.Abs(trfm.position.y - bottom) > maxSpeed)
            {
                destination = bottom;
                inMovement = true;
            }
        }
        else if (use == Use.Top)
        {
            if (Mathf.Abs(trfm.position.y - top) > maxSpeed)
            {
                destination = top;
                inMovement = true;
            }
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
            inMovement = true;
        }
    }

    void Move()
    {
        CameraController.SetTrauma(startTrauma);

        destination = GetFurtherDestination();

        epicInvokeInProcess = false;
        inMovement = true;
    }

    float GetFurtherDestination()
    {
        if (Mathf.Abs(trfm.position.y - top) < Mathf.Abs(trfm.position.y - bottom))
        {
            return bottom;
        }
        else
        {
            return top;
        }
    }

    float GetCloserDestination()
    {
        if (Mathf.Abs(trfm.position.y - top) < Mathf.Abs(trfm.position.y - bottom))
        {
            return top;
        }
        else
        {
            return bottom;
        }
    }
}
