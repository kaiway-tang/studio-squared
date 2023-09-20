using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : HPEntity
{
    [SerializeField] int triggerThreshold, transitionTime, openingDelay, panToGateDuration, deathCount;
    [SerializeField] Vector3 shiftSpeed;
    int timer;
    bool opened;
    // Start is called before the first frame update
    new void Start()
    {
        //DontDestroyOnLoad(gameObject);
        base.Start();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (timer > 0)
        {
            if (timer <= transitionTime)
            {
                trfm.position -= shiftSpeed;
                CameraController.SetTrauma(7);
            }
            if (timer == transitionTime) { CameraController.SetTrauma(20); }
            timer--;
        }
        else if (timer < 0)
        {
            if (timer >= -transitionTime)
            {
                trfm.position += shiftSpeed;

                if (timer < -5)
                {
                    trfm.position += shiftSpeed * 5;
                    timer += 5;
                }
            }

            if (timer == -1) { CameraController.SetTrauma(20); }

            timer++;
        }
    }

    public void Open(int delay = 0)
    {
        timer = transitionTime + delay;
    }

    public void Close(int delay = 0)
    {
        timer = -transitionTime - delay;
    }

    public void IncrementDeathCount()
    {
        deathCount++;
        if (deathCount == triggerThreshold)
        {
            Open(openingDelay);
            if (panToGateDuration > 0)
            {
                Invoke("QuePan", 1);
            }
        }
    }

    void QuePan()
    {
        CameraController.QueCameraPan(trfm.position, panToGateDuration, 0);
    }
}
