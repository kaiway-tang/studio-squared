using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] Transform cameraTrfm;
    [SerializeField] float trackingRate, rotationRate, moveIntensity, rotationIntensity;
    [SerializeField] Vector2 deadzoneDimensions;
    int mode;
    const int TRACKING_PLAYER = 0;

    static CameraController self;

    Vector3 vect3;

    private void Awake()
    {
        GameManager.cameraTrfm = trfm;
        self = GetComponent<CameraController>();
    }

    void Start()
    {
        trfm.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mode == TRACKING_PLAYER)
        {
            TrackTarget(GameManager.playerTrfm.position + Vector3.up);
        }

        ProcessTrauma();
    }

    float xDiff, yDiff;
    void TrackTarget(Vector2 targetPos)
    {
        xDiff = targetPos.x - trfm.position.x;
        yDiff = targetPos.y - trfm.position.y;

        if (Mathf.Abs(xDiff) > deadzoneDimensions.x)
        {
            if (xDiff > 0) xDiff -= deadzoneDimensions.x;
            else xDiff += deadzoneDimensions.x;

            vect3.x = xDiff * trackingRate;
        }
        else
        {
            vect3.x = 0;
        }

        if (Mathf.Abs(yDiff) > deadzoneDimensions.y)
        {
            if (yDiff > 0) yDiff -= deadzoneDimensions.y;
            else yDiff += deadzoneDimensions.y;

            vect3.y = yDiff * trackingRate;
        }
        else
        {
            vect3.y = 0;
        }

        vect3.z = 0;

        trfm.position += vect3;
    }

    [SerializeField] int trauma;
    public static void AddTrauma(int amount, int max = int.MaxValue)
    {
        self.trauma += amount;
        if (self.trauma > max)
        {
            self.trauma = max;
        }
    }
    public static void SetTrauma(int amount)
    {
        if (self.trauma < amount)
        {
            self.trauma = amount;
        }
    }

    float processedTrauma;
    Vector3 zVect3;
    void ProcessTrauma()
    {
        //rotational "recovery"
        if (cameraTrfm.localEulerAngles.z < .1f || cameraTrfm.localEulerAngles.z > 359.9f)
        {
            cameraTrfm.localEulerAngles = Vector3.zero;
        }
        else
        {
            if (cameraTrfm.localEulerAngles.z < 180) { zVect3.z = -cameraTrfm.localEulerAngles.z * rotationRate; }
            else { zVect3.z = (360 - cameraTrfm.localEulerAngles.z) * rotationRate; }
            cameraTrfm.localEulerAngles += zVect3;
        }

        cameraTrfm.position += (trfm.position - cameraTrfm.position) * .1f;

        //screen shake/rotation
        if (trauma > 0)
        {
            if (trauma > 48) //hard cap trauma at 40
            {
                processedTrauma = 2100;
            }
            else if (trauma > 30) //soft cap at 30 trauma
            {
                processedTrauma = 900 + 60 * (trauma - 30);
            }
            else
            {
                //amount of "shake" is proportional to trauma squared
                processedTrauma = trauma * trauma;
            }

            //generate random Translational offset for camera per tick
            vect3 = Random.insideUnitCircle.normalized * moveIntensity * processedTrauma;
            cameraTrfm.position += vect3;

            //generate random Rotational offset for camera per tick
            cameraTrfm.Rotate(Vector3.forward * rotationIntensity * (Random.Range(0, 2) * 2 - 1) * processedTrauma);

            //decrement trauma as a timer
            trauma--;
        }
    }
}
