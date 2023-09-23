using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Laser : MonoBehaviour
{
    [SerializeField] Camera CameraLateLatchMatrixType;

    [SerializeField] LineRenderer laser;

    [SerializeField] Transform firePoint;

    [SerializeField] private float fireDelay;
    private bool laserEnabled;
    [SerializeField] private LayerMask layerstohit;
    [SerializeField] GameObject startFX;
    [SerializeField] GameObject endFX;
    [SerializeField] private List<ParticleSystem> particles;

    private float remainingTime;
    // Start is called before the first frame update
    void Start()
    {
        laser.enabled = false;
        remainingTime = fireDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnableLaser();
        // if (remainingTime <= 0f)
        // {
        //     laserEnabled = !laserEnabled;
        //     remainingTime = fireDelay;
        //     
        // }
        // if (remainingTime > 0f)
        // {
        //     remainingTime -= Time.deltaTime;
        // }
        //
        // if (laserEnabled)
        // {
        //     EnableLaser();
        // }
        // else
        // {
        //     DisableLaser();
        // }
    }
    private void  EnableLaser()
    {
        laser.enabled = true;
        for (int i = 0; i < particles.Count;i++)
        {
            particles[i].Play();
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
