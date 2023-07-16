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

    int remainingJumps, flipLocked;

    int slashCooldown;

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
            DoJump();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            DoAttack();
        }
    }

    void DoAttack()
    {
        if (slashCooldown > 0) { return; }
        attackAnimator.Play();

        if (IsFacingLeft()) { basicAttack.Activate(1, 16); }
        else { basicAttack.Activate(0, 16); }

        LockFacing(18);
        slashCooldown = 25;
    }

    void DoJump()
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsOnGround())
        {
            remainingJumps = 1;
        }

        HandleHorizontalMovement();
        DecrementTimers();
    }

    void DecrementTimers()
    {
        if (flipLocked > 0) { flipLocked--; }
        if (slashCooldown > 0) { slashCooldown--; }
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

    public Vector2 GetPredictedPosition(float time)
    {
        return trfm.position;
    }

    new void SetFacing(bool direction)
    {
        if (flipLocked < 1) { base.SetFacing(direction); }
    }

    void LockFacing(int duration)
    {
        if (flipLocked < duration) { flipLocked = duration; }
    }
}
