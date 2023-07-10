using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] float trackingRate;
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
            vect3 = (GameManager.playerTrfm.position - trfm.position) * trackingRate;
            vect3.z = 0;
            trfm.position += vect3;
        }
    }
}
