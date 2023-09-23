using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Laser : MonoBehaviour
{
    [SerializeField] Camera CameraLateLatchMatrixType;

    [SerializeField] LineRenderer laser;

    [SerializeField] Transform firePoint;

    [SerializeField] private float fireDelay = 4;
    [SerializeField] private float chargeTime = 2;
    private bool laserEnabled;
    [SerializeField] private LayerMask layerstohit;
    [SerializeField] GameObject startFX;
    [SerializeField] GameObject endFX;
    [SerializeField] private List<ParticleSystem> particles;

    private float remainingTime;
    // Start is called before the first frame update
    void Start()
    {
        remainingTime = fireDelay;
        DisableLaser();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // EnableLaser();
        startFX.transform.position = transform.position;

        if (remainingTime <= 0f)
        {
            laserEnabled = !laserEnabled;
            remainingTime = fireDelay;
            
        }
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
        }
        // Debug.Log("remain:" + remainingTime);
        if (remainingTime < chargeTime && !laserEnabled)
        {
            if (!particles[0].isPlaying)
            {
                particles[0].Play();
                Debug.Log("warn");
            }
            
        }
        else
        {
            if (particles[0].isPlaying)
            {
                particles[0].Stop();
                // Debug.Log("warn");
            }
        }
        if (laserEnabled)
        {
            EnableLaser();
        }
        else
        {
            DisableLaser();
        }
    }
    private void  EnableLaser()
    {
        Debug.Log("laser");
        laser.enabled = true;
        for (int i = 1; i < particles.Count;i++)
        {
            // if (i == 1)
            // {
            //     continue;
            // }
            if (!particles[i].isPlaying)
            {
                particles[i].Play();
            }
            
        }
        
        startFX.transform.position = transform.position;
        // startFX.transform.rotation = transform.rotation;
        Debug.Log(startFX.transform.rotation);
        Debug.Log(transform.rotation);
        laser.SetPosition(0,firePoint.position);
        Vector2 direction = transform.right;
        RaycastHit2D hit = Physics2D.Raycast((Vector2) transform.position, direction.normalized, 100f, layerstohit);
        // Debug.DrawRay(transform.position, direction.normalized * 100f, Color.red);

        if (hit)
        {
            laser.SetPosition(1, hit.point);
        }
        else
        {
            laser.SetPosition(1, (Vector2) transform.position + direction * 100f);
        }

        endFX.transform.position = laser.GetPosition(1);
        // endFX.transform.LookAt(transform.position);

    }

    private void DisableLaser()
    {
        for (int i = 1; i < particles.Count;i++)
        {
            if (particles[i].isPlaying)
            {
                particles[i].Stop();
            }
        }
        laser.enabled = false;
        
    }

    // private void fillList()
    // {
    //     for (int i = 0; i < startFX.transform.childCount;i++)
    //     {
    //         var ps = startFX.transform.GetChild(i).GetComponent<ParticleSystem>();
    //         if (ps != null)
    //         {
    //             
    //         }
    //     }
    // }
    
}
