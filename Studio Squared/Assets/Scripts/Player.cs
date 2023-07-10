using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MobileEntity
{
    [SerializeField] float acceleration, maxSpeed, friction, jumpPower;

    private void Awake()
    {
        GameManager.playerTrfm = trfm;
    }

    new void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rb.velocity.y < jumpPower)
            {
                SetYVelocity(jumpPower);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (!Input.GetKey(KeyCode.A))
            {
                AddXVelocity(acceleration, maxSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            AddXVelocity(-acceleration, -maxSpeed);
        }
        else
        {
            ApplyXFriction(friction);
        }
    }
}
