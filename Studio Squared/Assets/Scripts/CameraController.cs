using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] float trackingRate;
    [SerializeField] Vector2 deadzoneDimensions;
    int mode;
    const int TRACKING_PLAYER = 0;

    Vector3 vect3;

    private void Awake()
    {
        GameManager.cameraTrfm = trfm;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mode == TRACKING_PLAYER)
        {
            TrackTarget(GameManager.playerTrfm);
        }
    }

    float xDiff, yDiff;
    void TrackTarget(Transform target)
    {
        xDiff = target.position.x - trfm.position.x;
        yDiff = target.position.y - trfm.position.y;

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
}
