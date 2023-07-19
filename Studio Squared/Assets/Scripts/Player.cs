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
        jumpPower, wallJumpSpeed;

    int remainingJumps, flipLocked;

    int wallKickWindow, slashCooldown;

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
            JumpPressed();
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

    void JumpPressed()
    {
        wallKickWindow = 4;
        if (!HandleWallKickInput())
        {
            AttemptJump();
        }
    }

    void AttemptJump()
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

    bool HandleWallKickInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (!Input.GetKey(KeyCode.A))
            {
                if (TerrainTriggerTouching(2))
                {
                    WallJump(RIGHT);
                    return true;
                }
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (TerrainTriggerTouching(1))
            {
                WallJump(LEFT);
                return true;
            }
        }
        return false;
    }

    void WallJump(bool direction)
    {
        if (wallKickWindow > 0)
        {
            remainingJumps++;
            wallKickWindow = 0;
        }
        
        Jump();
        if (direction == RIGHT)
        {
            AddXVelocity(99, wallJumpSpeed);
        } else
        {
            AddXVelocity(-99, -wallJumpSpeed);
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
        if (wallKickWindow > 0)
        {
            HandleWallKickInput();
            wallKickWindow--;
        }
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
                if (!AddXVelocity(ActiveAcceleration(), maxSpeed))
                {
                    ApplyXFriction(ActiveFriction());
                }
                SetFacing(RIGHT);
                return;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!AddXVelocity(-ActiveAcceleration(), -maxSpeed))
            {
                ApplyXFriction(ActiveFriction());
            }
            SetFacing(LEFT);
            return;
        }

        ApplyXFriction(ActiveFriction());
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
