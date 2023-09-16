using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] Transform cameraTrfm;
    [SerializeField] float trackingRate, returnRate, rotationRate, moveIntensity, rotationIntensity;
    [SerializeField] Vector2 deadzoneDimensions;
    int mode;
    const int MOVEMENT = 0, COMBAT = 1;

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
        if (mode == MOVEMENT)
        {
            HandleDefaultTracking();
            //TrackTarget(GameManager.playerTrfm.position + Vector3.up);
        }
        else if (mode == COMBAT)
        {
            combatTimer--;
            if (combatTimer < 1)
            {
                ExitCombat();
            }

            TrackWithDeadzone(GameManager.playerTrfm.position, trackingRate);
        }

        ProcessTrauma();
    }

    Vector2 lastPOI;
    void HandleDefaultTracking()
    {
        if (Player.inHorizontalMovement)
        {
            lastPOI = Player.GetPredictedPosition(.3f);
            TrackWithDeadzone((Vector3)lastPOI + Vector3.up * 2, trackingRate);
        }
        else
        {
            vect3.x = lastPOI.x; vect3.y = GameManager.playerTrfm.position.y + 2;
            TrackWithDeadzone(vect3, trackingRate);
        }
    }

    float xDiff, yDiff;

    void TrackTarget(Vector2 targetPos, float rate)
    {
        vect3.x = (targetPos.x - trfm.position.x) * rate;
        vect3.y = (targetPos.y - trfm.position.y) * rate;
        vect3.z = 0;

        trfm.position += vect3;
    }

    void TrackWithDeadzone(Vector2 targetPos, float rate)
    {
        xDiff = targetPos.x - trfm.position.x;
        yDiff = targetPos.y - trfm.position.y;

        if (Mathf.Abs(xDiff) > deadzoneDimensions.x)
        {
            if (xDiff > 0) xDiff -= deadzoneDimensions.x;
            else xDiff += deadzoneDimensions.x;

            vect3.x = xDiff * rate;
        }
        else
        {
            vect3.x = 0;
        }

        if (Mathf.Abs(yDiff) > deadzoneDimensions.y)
        {
            if (yDiff > 0) yDiff -= deadzoneDimensions.y;
            else yDiff += deadzoneDimensions.y;

            vect3.y = yDiff * rate;
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

    int combatTimer;
    public static void EnterCombat()
    {
        self.mode = COMBAT;

        self.vect3.x = 2; self.vect3.y = 3;
        self.deadzoneDimensions = self.vect3;

        self.combatTimer = 50;
    }

    public static void RefreshCombat()
    {
        if (self.mode == COMBAT && self.combatTimer < 50)
        {
            self.combatTimer = 50;
        }
    }

    public static void ExitCombat()
    {
        self.mode = MOVEMENT;
        self.lastPOI = Player.GetPredictedPosition(.3f);

        self.vect3.x = 1.5f; self.vect3.y = 1;
        self.deadzoneDimensions = self.vect3;
    }
}
