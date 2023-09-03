using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MobileEntity
{
    [SerializeField] GameObject circleObj;
    [SerializeField] SimpleAnimator attackAnimator;
    [SerializeField] Attack basicAttack, dashSlashAttack;
    
    [SerializeField] float
        groundedAcceleration, aerialAcceleration, maxSpeed,
        groundedFriction, aerialFriction,
        jumpPower, wallJumpSpeed, dashSpeed, dashFriction,
        flatYDashVelocity, YVelocityFactor;

    int remainingJumps, facingLocked, movementLocked;
    bool refundableJump;

    [SerializeField] private ParticleSystem dashEffect;

    [SerializeField] int wallJumpWindow, slashCooldown, dashCooldown;
    [SerializeField] TrailRenderer wallJumpTrail;
    [SerializeField] CircleCollider2D hurtbox;
    [SerializeField] GameObject sparkle;
    [SerializeField] TrailRenderer dashSlashTrail;
    int hurtboxDisable, attackCharge, dashSlashRecovery, dashSlashRange = 9;
    int trailTimer;
    [SerializeField] private Vector2 inputVector;

    [SerializeField] ObjectPooler perfectDodgePooler;
    [SerializeField] ParticleSystem healFX;
    [SerializeField] PlayerAnimator animator;

    public static int mana;


    private bool frozen;


    private void Awake()
    {
        GameManager.playerTrfm = trfm;
        self = GetComponent<Player>();

        //playerInput = GetComponent<PlayerInput>();
        //moveaAction = playerInput.actions["Move"];
    }

    new void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (frozen)
        {
            return; //skip movement
        }
        if (PlayerInput.JumpPressed())
        {
            OnJump();
        }

        if (PlayerInput.AttackPressed())
        {
            OnAttack();
        }
        if (PlayerInput.AttackReleased())
        {
            if (attackCharge > 6)
            {
                LockMovement(false);
                if (attackCharge > 49)
                {
                    DashSlash();
                }
            }
            attackCharge = 0;
        }
    }

    void OnDash()
    {
        if (dashCooldown > 0) { return; }

        DisableHurtbox();
        animator.QueAnimation(animator.Dash, 16);

        rb.velocity = PlayerInput.GetVectorInput() * dashSpeed;

        if (Mathf.Abs(rb.velocity.y) < .01f)
        {
            if (Mathf.Abs(rb.velocity.x) < .01f)
            {
                if (IsFacingLeft())
                {
                    SetXVelocity(-dashSpeed);
                }
                else
                {
                    SetXVelocity(dashSpeed);
                }
            }

            SetYVelocity(flatYDashVelocity);
        }
        else if (rb.velocity.y > 0)
        {
            SetYVelocity(rb.velocity.y * YVelocityFactor);
        }

        dashCooldown = 75;

        wallJumpTrail.emitting = true;
        dashEffect.Play();
        
        trailTimer = 10;
    }

    void DashSlash()
    {

        dashSlashRecovery = 25;
        LockMovement(true);
        LockFacing(25);
        dashSlashTrail.emitting = true;
        dashEffect.Play();
        SetYVelocity(0);
        animator.QueAnimation(animator.Dash, 16);

        RaycastHit2D hitInfo;
        if (IsFacingLeft())
        {
            if (hitInfo = Physics2D.Linecast(trfm.position, trfm.position - Vector3.right * dashSlashRange, GameManager.terrainLayerMask))
            {
                trfm.position += Vector3.right * -(hitInfo.distance - 1);
                Debug.Log("hit: " + hitInfo.collider.gameObject);
            }
            else
            {
                trfm.position += Vector3.right * -dashSlashRange;
            }
            dashSlashAttack.Activate(1, 5);
        }
        else
        {
            if (hitInfo = Physics2D.Linecast(trfm.position, trfm.position + Vector3.right * dashSlashRange, GameManager.terrainLayerMask))
            {
                trfm.position += Vector3.right * (hitInfo.distance - 1);
            }
            else
            {
                trfm.position += Vector3.right * dashSlashRange;
            }
            dashSlashAttack.Activate(0, 5);
        }
    }

    private void OnAttack ()  
    {
        if (slashCooldown > 0) { return; }
        attackAnimator.Play();

        if (IsFacingLeft()) { basicAttack.Activate(1, 12); }
        else { basicAttack.Activate(0, 12); }

        LockFacing(18);
        slashCooldown = 25;
    }

    private void OnJump()
    {
        if (!HandleWallJumpInput())
        {
            wallJumpWindow = 3;
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
            refundableJump = true;
            remainingJumps--;
        }
    }

    bool HandleWallJumpInput()
    {
        inputVector = PlayerInput.GetVectorInput();

        if (inputVector.x > 0.01f)
        {
            if (TerrainTriggerTouching(2))
            {
                WallJump(RIGHT);
                return true;
            }
        }
        else if (inputVector.x < -.01f)
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
        wallJumpTrail.emitting = true;
        trailTimer = 14;

        if (wallJumpWindow < 1 || (remainingJumps < 1 && !refundableJump) || true) //NOTE: disabled ( || true)
        {
            Jump();
        }
        if (wallJumpWindow > 0)
        {
            if (refundableJump)
            {
                remainingJumps++;
                refundableJump = false;
            }
            wallJumpWindow = 0;
        }

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

        if (PlayerInput.AttackHeld())
        {
            if (attackCharge < 50)
            {
                attackCharge++;
                if (attackCharge == 7)
                {
                    LockMovement(true);
                }

                if (attackCharge > 49)
                {
                    Instantiate(sparkle, trfm.position, trfm.rotation);
                }
            }
        }

        if (PlayerInput.DashHeld()) { OnDash(); }

        if (frozen)
        {
            return; //skip movement - TODO may need to move
        }

        HandleHorizontalMovement();
        DecrementTimers();
        HandlePositionPredicting();
        HandleAnimations();
    }

    void HandleAnimations()
    {
        if (!IsOnGround())
        {
            ApplyAerialAnimations();
        }
    }

    void ApplyAerialAnimations()
    {
        if (rb.velocity.y < 0)
        {
            animator.RequestAnimatorState(animator.Fall);
        }
        else
        {
            animator.RequestAnimatorState(animator.Jump);
        }
    }

    void DecrementTimers()
    {
        if (dashSlashRecovery > 0)
        {
            dashSlashRecovery--;
            if (dashSlashRecovery == 23)
            {
                dashSlashTrail.emitting = false;
            }
            if (dashSlashRecovery < 1)
            {
                LockMovement(false);            
            }
        }
        if (facingLocked > 0) { facingLocked--; }
        if (slashCooldown > 0) { slashCooldown--; }
        if (dashCooldown > 0)
        {
            dashCooldown--;
            if (dashCooldown < 72 && dashCooldown > 62 && Mathf.Abs(rb.velocity.magnitude) > wallJumpSpeed)
            {
                if (IsOnGround())
                {
                    ApplyDirectionalFriction(dashFriction - groundedFriction);
                }
                else
                {
                    ApplyDirectionalFriction(dashFriction);
                }
            }
            else if (dashCooldown == 55)
            {
                EnableHurtbox();
            }
        }

        if (wallJumpWindow > 0)
        {
            HandleWallJumpInput();
            if (wallJumpWindow == 0)
            {
                refundableJump = false;
            }
            wallJumpWindow--;
        }

        if (trailTimer > 0)
        {
            trailTimer--;
            if (trailTimer == 0)
            {
                wallJumpTrail.emitting = false;
            }
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
        if (frozen)
        {
            ApplyXFriction(ActiveFriction());
            return;
        }

        inputVector = PlayerInput.GetVectorInput();

        if (inputVector.x > 0.01f)
        {

            if (!AddXVelocity(ActiveAcceleration(), maxSpeed))
            {
                ApplyXFriction(ActiveFriction());
            }

            animator.RequestAnimatorState(animator.Run);
            SetFacing(RIGHT);
            return;
        }
        else if (inputVector.x < -.01f)
        {
            if (!AddXVelocity(-ActiveAcceleration(), -maxSpeed))
            {
                ApplyXFriction(ActiveFriction());
            }

            animator.RequestAnimatorState(animator.Run);
            SetFacing(LEFT);
            return;
        }
        else
        {
            animator.RequestAnimatorState(animator.Idle);
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

    new void SetFacing(bool direction)
    {
        if (facingLocked < 1) { base.SetFacing(direction); }
    }

    void LockFacing(int duration)
    {
        if (facingLocked < duration) { facingLocked = duration; }
    }

    void LockMovement(bool _lock)
    {
        if (_lock) { movementLocked++; }
        else { movementLocked--; }
    }

    void EnableHurtbox()
    {
        hurtboxDisable--;
        if (hurtboxDisable < 1)
        {
            hurtbox.enabled = true;
            hurtboxDisable = 0;
        }
    }
    void DisableHurtbox()
    {
        if (hurtboxDisable < 1)
        {
            perfectDodgePooler.Instantiate(trfm.position, 0);
            hurtbox.enabled = false;
        }
        hurtboxDisable++;
    }

    protected override void OnDamageTaken(int amount)
    {
        if ((int)(amount) < 8)
        {
            CameraController.SetTrauma(12);
            HUDManager.SetVignetteOpacity(.32f);
        }
        else
        {
            CameraController.SetTrauma((int)(amount * 1.5f));
            HUDManager.SetVignetteOpacity(amount * .04f);
        }
    }

    protected override void OnHeal(int amount)
    {
        healFX.Play();
    }


    public static Player self;

    int velocityLogTimer;
    Vector2[] loggedVelocities = new Vector2[4];
    Vector2 averageVelocity;
    int nextIndex;
    void HandlePositionPredicting()
    {
        if (velocityLogTimer > 0) { velocityLogTimer--; }
        else
        {
            loggedVelocities[nextIndex] = rb.velocity;
            averageVelocity = (loggedVelocities[0] + loggedVelocities[1] + loggedVelocities[2] + loggedVelocities[3] + loggedVelocities[nextIndex]) * .2f;

            nextIndex++;
            if (nextIndex > 3) { nextIndex = 0; }

            velocityLogTimer = 5;
        }
    }

    static Vector2 vect2;
    public static Vector2 GetPredictedPosition(float seconds, bool debugDot = false)
    {
        vect2 = self.trfm.position;
        if (seconds > 1) { seconds = 1; }
        if (debugDot)
        {
            Instantiate(self.circleObj, GetPredictedPosition(seconds), Quaternion.identity);
        }
        return self.averageVelocity * seconds + vect2;
    }




    //freeze player
    public void SetFrozen(bool setTo)
    {
        Debug.Log("SHOUDL SET FROZEN");
        frozen = setTo;
    }
}

