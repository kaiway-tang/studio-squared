using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MobileEntity
{
    [SerializeField] SimpleAnimator attackAnimator;
    [SerializeField] Attack basicAttack;

    [SerializeField] float
        groundedAcceleration, aerialAcceleration, maxSpeed,
        groundedFriction, aerialFriction,
        jumpPower;

    [SerializeField] int remainingJumps;

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
            if (IsOnGround())
            {
                Jump();
            }
            else if (remainingJumps > 0)
            {
                Jump();
                remainingJumps--;
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            attackAnimator.Play();

            if (IsFacingLeft()) { basicAttack.Activate(1, 5); }
            else { basicAttack.Activate(0, 5); }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsOnGround())
        {
            remainingJumps = 1;
        }

        HandleHorizontalMovement();
    }

    float ActiveAcceleration() //can modify later to handle speed/slow effects
    {
        if (IsOnGround()) { return groundedAcceleration; }
        return aerialAcceleration;
    }

    float ActiveFriction()
    {
        if (IsOnGround()) { return groundedFriction; }
        return aerialFriction;
    }

    void HandleHorizontalMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (!Input.GetKey(KeyCode.A))
            {
                AddXVelocity(ActiveAcceleration(), maxSpeed);
                SetFacing(RIGHT);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            AddXVelocity(-ActiveAcceleration(), -maxSpeed);
            SetFacing(LEFT);
        }
        else
        {
            ApplyXFriction(ActiveFriction());
        }
    }

    void Jump()
    {
        if (rb.velocity.y < jumpPower)
        {
            SetYVelocity(jumpPower);
        }
    }
}
