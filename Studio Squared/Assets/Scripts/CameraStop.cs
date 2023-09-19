using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStop : MonoBehaviour
{
    protected void Start()
    {
        Destroy(GetComponent<SpriteRenderer>());
    }

    protected void TriggerEnteredByPlayer()
    {
        CameraController.SetMode(CameraController.XLOCKED);
    }

    protected void TriggerExitedByPlayer()
    {
        //CameraController.SetMode(CameraController.MOVEMENT);
        CameraController.SetLastMode();
    }
}
