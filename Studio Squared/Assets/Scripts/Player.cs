using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MobileEntity
{
    [SerializeField] GameObject circleObj, predictDot;
    [SerializeField] SimpleAnimator[] attackAnimator;
    [SerializeField] PlayerAttack basicAttack1, basicAttack2, dashSlashAttack, slideAttack;
    
    [SerializeField] float
        groundedAcceleration, aerialAcceleration, maxSpeed,
        groundedFriction, aerialFriction,
        jumpPower, wallJumpSpeed, dashSpeed, dashFriction,
        flatYDashVelocity, YVelocityFactor;

    int remainingJumps, facingLocked, movementLocked, attackQue;
    bool refundableJump, releaseAttacked;

    [SerializeField] private ParticleSystem dashEffect;

    [SerializeField] int wallJumpWindow, slashCooldown, slashComboWindow, releaseAttackWindow, dashCooldown, castCooldown, castWindup;
    [SerializeField] TrailRenderer wallJumpTrail;
    [SerializeField] CircleCollider2D hurtbox;
    [SerializeField] GameObject sparkle;
    [SerializeField] TrailRenderer dashSlashTrail;
    int hurtboxDisable, attackCharge, dashSlashRecovery, dashSlashRange = 9;
    int trailTimer;
    [SerializeField] private Vector2 inputVector;

    [SerializeField] ObjectPooler perfectDodgePooler;
    [SerializeField] ParticleSystem healFX, jumpFX, frontTurnFX, backTurnFX, runFX, dashRefreshFX;
    public PlayerAnimator animator;

    [SerializeField] GameObject lightningBolt, castChargeFX;
    [SerializeField] GameObject fullManaIndicator;

    public static int mana, maxMana = 120, gravityDisable, fallingTimer, scytheTimer;

    [SerializeField] bool unlockAllAbilities;
    public static bool hasDoubleJump, hasDash, hasWallJump, hasDashSlash, hasCast, hasDoubleSlash;

    [SerializeField] SpriteRenderer hpHUD, spriteRenderer;
    [SerializeField] Sprite[] hpHudSprites;
    [SerializeField] Transform manaFill;
    [SerializeField] Fader hpBarDmgFX;
    [SerializeField] FloatingScythe scytheScript;
    [SerializeField] SpinningScythe spinningScythe;
    public ObjectPooler dJumpRingPooler, perfectDodgeRingPooler;

    private bool frozen;
    private int playerTotalCoins;


    static bool initStartComplete;
    private void Awake()
    {
        hasDoubleSlash = true;

        GameManager.playerTrfm = trfm;
        self = GetComponent<Player>();

        if (!initStartComplete)
        {
            GameManager.playerHP = Player.self.maxHP;
            initStartComplete = true;
        }

        playerTotalCoins = 0;

        //playerInput = GetComponent<PlayerInput>();
        //moveaAction = playerInput.actions["Move"];
    }

    new void Start()
    {
        base.Start();

        if (unlockAllAbilities)
        {
            hasDoubleJump = true;
            hasDash = true;
            hasWallJump = true;
            hasDashSlash = true;
            hasCast = true;
        }

        HP = GameManager.playerHP;
        UpdateHPHUD();
    }

    static bool cheatsOn = true;
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
            if (attackCharge > 49)
            {
                DashSlash();
            }
            attackCharge = 0;
        }

        if (cheatsOn && Input.GetKeyDown(KeyCode.Minus))
        {
            TakeDamage(2, EntityType.Neutral);
        }

        if (lookDownTimer > 39 && PlayerInput.DownReleased())
        {
            lookDownTimer = 0;
            CameraController.SetMode(CameraController.MOVEMENT);
        }
    }

    int baseDashCooldown = 60;
    void OnDash()
    {
        if (dashCooldown > 0 || !hasDash) { return; }

        DisableHurtbox();

        if (IsOnGround() || true) { slideAttack.Activate(0, 16); }

        animator.QueAnimation(animator.Slide, 16);
        spriteRenderer.color = Color.white * .4f + Color.black * .6f;

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

        dashCooldown = baseDashCooldown;

        wallJumpTrail.emitting = true;
        dashEffect.Play();
        
        trailTimer = 10;
    }

    [SerializeField] Vector2 castKnockback;
    void OnCast()
    {
        if (castCooldown < 1 && mana >= 40 && hasCast)
        {
            Instantiate(castChargeFX, trfm.position, Quaternion.identity);
            LockMovement(15);
            rb.velocity = Vector2.zero;
            castCooldown = 40;
            AddMana(-40);
        }
    }

    void DashSlash()
    {
        dashSlashRecovery = 25;
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


    int comboCooldown = 15;
    private void OnAttack ()  
    {
        FloatingScythe.trailEnabled = true;

        if (releaseAttackWindow > 0)
        {
            if (slashCooldown < 15)
            {
                AlignFacing(true);

                spinningScythe.Init(trfm.position, IsFacingLeft());
                animator.QueAnimation(animator.Cast, 25);

                HandleAttackMovement();

                LockMovement(15);
                LockFacing(15);

                releaseAttacked = true;
                releaseAttackWindow = 0;
                slashCooldown = 35;
                attackQue = 0;
            }
            else
            {
                if (attackQue < 1) { attackQue++; }
            }
        }
        else if (slashCooldown > 0) 
        {
            if (slashComboWindow > 0)
            {
                if (slashCooldown < comboCooldown)
                {
                    scytheScript.Dematerialize(30);
                    attackAnimator[1].Play();
                    animator.QueAnimation(animator.Attack2, 17);

                    HandleAttackMovement();

                    slashComboWindow = 0;
                    releaseAttackWindow = 25;
                    slashCooldown = 25;
                    attackQue--;
                }
                else
                {
                    if (attackQue < 2) { attackQue++; }
                }
            }
        }
        else
        {
            scytheScript.Dematerialize(65);

            attackAnimator[0].Play();
            animator.QueAnimation(animator.Attack1, 17);

            HandleAttackMovement();

            slashCooldown = 25;
            slashComboWindow = 20;
        }
    }

    void HandleAttackMovement()
    {
        LockFacing(12);
        if (IsOnGround())
        {
            LockMovement(9);
            if (IsFacingLeft())
            {
                if (PlayerInput.LeftHeld()) { AddXVelocity(-25, -25); }
                else if (!PlayerInput.RightHeld()) { AddXVelocity(-10, -10); }
            }
            else
            {
                if (PlayerInput.RightHeld()) { AddXVelocity(25, 25); }
                else if (!PlayerInput.LeftHeld()) { AddXVelocity(10, 10); }
            }
        }
    }

    private void OnJump()
    {
        if (IsDisabled()) { return; }
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
            jumpFX.Play();
            Jump();
        }
        else if (remainingJumps > 0 && hasDoubleJump)
        {
            dJumpRingPooler.Instantiate(trfm.position, 0);
            animator.QueAnimation(animator.Roll, 17);
            Jump();
            refundableJump = true;
            remainingJumps--;
        }
    }

    bool HandleWallJumpInput()
    {
        if (!hasWallJump) { return false; }
        
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
        animator.QueAnimation(animator.Roll, 17);
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
    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (IsOnGround())
        {
            remainingJumps = 1;

            if (fallingTimer > 0)
            {
                if (fallingTimer > 14)
                {
                    CameraController.ResetPOI();
                    CameraController.SetMode(CameraController.MOVEMENT);
                }

                if (fallingTimer > 49)
                {
                    CameraController.SetTrauma(15);
                    Stun(25);
                }
                fallingTimer = 0;
            }
        }
        else
        {
            SetRunFXActive(false);

            if (rb.velocity.y < -30)
            {
                if (fallingTimer < 50 && !Physics2D.Linecast(trfm.position, trfm.position + Vector3.down * 8, GameManager.terrainLayerMask))
                {
                    fallingTimer++;
                    if (fallingTimer == 15) { CameraController.SetMode(CameraController.FALLING); }
                }
                SetYVelocity(-30);
            }
        }

        if (hasDashSlash && PlayerInput.AttackHeld())
        {
            if (attackCharge < 50)
            {
                attackCharge++;

                if (attackCharge > 49)
                {
                    Instantiate(sparkle, trfm.position, trfm.rotation);
                }
            }
        }

        if (PlayerInput.DashHeld()) { OnDash(); }

        if (PlayerInput.CastHeld())
        {
            OnCast();
        }

        HandleHorizontalMovement();
        DecrementTimers();
        HandlePositionTracking();
        HandleAnimations();
        HandleCameraPanning();
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
        if (Mathf.Abs(rb.velocity.y) < .1f)
        {
            animator.RequestAnimatorState(animator.Idle);
        }
        else if (rb.velocity.y > 0)        
        {
            animator.RequestAnimatorState(animator.Jump);
        }
        else
        {
            animator.RequestAnimatorState(animator.Fall);
        }
    }

    void DecrementTimers()
    {
        if (releaseAttackWindow > 0) { releaseAttackWindow--; }
        if (slashComboWindow > 0) { slashComboWindow--; }
        if (movementLocked > 0) { movementLocked--; }
        if (dashSlashRecovery > 0)
        {
            dashSlashRecovery--;
            if (dashSlashRecovery == 23)
            {
                dashSlashTrail.emitting = false;
            }
        }
        if (facingLocked > 0) { facingLocked--; }
        if (slashCooldown > 0)
        {
            slashCooldown--;

            if (slashCooldown == 20)
            {
                if (!releaseAttacked)
                {
                    if (slashComboWindow > 0)
                    {
                        if (IsFacingLeft())
                        {
                            basicAttack1.Activate(1, 7);
                        }
                        else
                        {
                            basicAttack1.Activate(0, 7);
                        }
                    }
                    else
                    {
                        if (IsFacingLeft())
                        {
                            basicAttack2.Activate(1, 7);
                        }
                        else
                        {
                            basicAttack2.Activate(0, 7);
                        }
                    }
                }
                else
                {
                    releaseAttacked = false;
                }
            }

            if (attackQue > 0 && slashCooldown < comboCooldown)
            {
                OnAttack();
            }
        }
        if (dashCooldown > 0)
        {
            dashCooldown--;
            if (dashCooldown < baseDashCooldown - 3 && dashCooldown > baseDashCooldown - 13 && Mathf.Abs(rb.velocity.magnitude) > wallJumpSpeed)
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
            else if (dashCooldown == baseDashCooldown - 20)
            {
                spriteRenderer.color = Color.white;
                EnableHurtbox();
            }

            if (dashCooldown == 0)
            {
                dashRefreshFX.Play();
            }
        }

        if (castCooldown > 0)
        {
            if (castCooldown > 25)
            {
                rb.velocity = Vector2.zero;
            }
            if (castCooldown == 25)
            {
                GameManager.LightningPtclsPooler.Instantiate(trfm.position);
                CameraController.SetTrauma(16);
                if (IsFacingRight())
                {
                    TakeKnockback(Vector2.right * -castKnockback.x + Vector2.up * castKnockback.y);
                    Instantiate(lightningBolt, trfm.position + Vector3.right * 8, trfm.rotation).transform.Rotate(Vector3.forward * -90);
                }
                else
                {
                    TakeKnockback(castKnockback);
                    Instantiate(lightningBolt, trfm.position - Vector3.right * 8, trfm.rotation).transform.Rotate(Vector3.forward * 90);
                }
            }
            castCooldown--;
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

    #region misc_systems

    int lookDownTimer;
    void HandleCameraPanning()
    {
        if (PlayerInput.DownHeld() && fallingTimer < 1)
        {
            lookDownTimer++;
            if (lookDownTimer > 40)
            {
                CameraController.SetMode(CameraController.LOOK_DOWN);
            }
        }
        else if (lookDownTimer > 0) { lookDownTimer--; }
    }

    public void SetGravityActive(bool active)
    {
        if (active)
        {
            gravityDisable--;
            if (gravityDisable < 1)
            {
                gravityDisable = 0;
                rb.gravityScale = 8;
            }
        }
        else
        {
            gravityDisable++;
            rb.gravityScale = 0;
        }
    }

    void SetRunFXActive(bool active)
    {
        if (active != runFXPlaying)
        {
            if (active) { runFX.Play(); }
            else { runFX.Stop(); }

            runFXPlaying = active;
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

    new bool SetFacing(bool direction)
    {
        if (facingLocked < 1) { return base.SetFacing(direction); }
        return false;
    }

    void AlignFacing(bool p_override = false)
    {
        if (p_override)
        {
            if (PlayerInput.RightHeld()) { base.SetFacing(RIGHT); }
            if (PlayerInput.LeftHeld()) { base.SetFacing(LEFT); }
        }
        else
        {
            if (PlayerInput.RightHeld()) { SetFacing(RIGHT); }
            if (PlayerInput.LeftHeld()) { SetFacing(LEFT); }
        }
    }

    void LockFacing(int duration)
    {
        if (facingLocked < duration) { facingLocked = duration; }
    }

    public static void LockMovement(int duration)
    {
        if (self.movementLocked < duration) { self.movementLocked = duration; }
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

    bool runFXPlaying;
    void HandleHorizontalMovement()
    {
        if (IsDisabled() || movementLocked > 0)
        {
            animator.RequestAnimatorState(animator.Idle);
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

            if (SetFacing(RIGHT) && IsOnGround() && rb.velocity.x < maxSpeed * -.5f)
            {
                backTurnFX.Play();
            }

            if (IsOnGround()) { SetRunFXActive(true); }
            return;
        }
        else if (inputVector.x < -.01f)
        {
            if (!AddXVelocity(-ActiveAcceleration(), -maxSpeed))
            {
                ApplyXFriction(ActiveFriction());
            }

            animator.RequestAnimatorState(animator.Run);
            if (SetFacing(LEFT) && IsOnGround() && rb.velocity.x > maxSpeed * .5f)
            {
                backTurnFX.Play();
            }

            if (IsOnGround()) { SetRunFXActive(true); }
            return;
        }
        else
        {
            SetRunFXActive(false);
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

    #endregion

    protected override void OnDamageTaken(int amount, int result)
    {
        CameraController.SetTrauma(12 + amount * 4); //10 + 5x
        HUDManager.SetVignetteOpacity(.4f + amount * .1f); //.3f + .2fx

        hpBarDmgFX.FadeIn(.08f, .7f + amount * .05f, true);
        UpdateHPHUD();

        if (result == DEAD)
        {
            Die();
        }
    }

    void Die()
    {
        Stun(200);
        SetGravityActive(false);
        SetInvulnerable(100);
        rb.velocity = Vector2.zero;

        spriteRenderer.sortingLayerName = "UI";
        spriteRenderer.sortingOrder = 650;
        animator.QueAnimation(animator.Death, 200);

        CameraController.SetTrauma(20);
        HUDManager.SetBlackCoverOpacity(1);
        //gameObject.SetActive(false);
        Invoke("Respawn", .3f);
    }

    void Respawn()
    {
        HP = maxHP;
        GameManager.LoadScene();
    }

    void UpdateHPHUD()
    {
        if (HP >= 12)
        {
            hpHUD.sprite = hpHudSprites[12];
        }
        else if (HP > 0)
        {
            hpHUD.sprite = hpHudSprites[HP];
        }
        else
        {
            hpHUD.sprite = hpHudSprites[0];
        }
    }

    protected override void OnHeal(int amount)
    {
        healFX.Play();
        UpdateHPHUD();
    }


    public static Player self;

    int velocityLogTimer;
    Vector2[] loggedVelocities = new Vector2[4];
    Vector2 averageVelocity;
    int nextIndex;
    public static bool inHorizontalMovement;
    float lastX;

    void HandlePositionTracking()
    {
        inHorizontalMovement = Mathf.Abs(trfm.position.x - lastX) > .1f;
        lastX = trfm.position.x;

        if (velocityLogTimer > 0) { velocityLogTimer--; }
        else
        {
            loggedVelocities[nextIndex] = rb.velocity;
            averageVelocity = (loggedVelocities[0] + loggedVelocities[1] + loggedVelocities[2] + loggedVelocities[3] + loggedVelocities[nextIndex]) * .2f;

            nextIndex++;
            if (nextIndex > 3) { nextIndex = 0; }
            velocityLogTimer = 5;

            predictDot.transform.position = GetPredictedPosition(.5f);
        }
    }

    static Vector2 vect2;
    public static Vector2 GetPredictedPosition(float seconds, bool showPosition = false, bool useY = false)
    {
        vect2 = self.trfm.position;
        if (seconds > 1) { seconds = 1; }
        if (showPosition)
        {
            Instantiate(self.circleObj, GetPredictedPosition(seconds), Quaternion.identity);
        }
        if (useY)
        {
            if (self.rb.velocity.y > 0)
            {
                vect2.x += self.averageVelocity.x * seconds;
                vect2.y = -self.rb.gravityScale * seconds * seconds + self.averageVelocity.y * seconds + self.trfm.position.y;
                return vect2;
            }
            else
            {
                return self.averageVelocity * seconds + vect2;
            }
        }
        else
        {
            vect2.x += self.averageVelocity.x * seconds;
            return vect2;
        }
    }

    bool IsDisabled()
    {
        return frozen || stunned > 0;
    }
    public static void AddMana(int amount)
    {
        if (!hasCast) { return; }
        mana += amount;
        if (mana > maxMana)
        {
            //self.fullManaIndicator.SetActive(true);
            mana = maxMana;
        }

        if (mana < 1)
        {
            self.manaFill.localPosition = new Vector3(0, -6, 0);
        }
        else
        {
            self.manaFill.localPosition = new Vector3(0, -5 + (float)mana / maxMana * 5, 0);
        }
    }

    public static string nextScene = "";
    private void OnBecameInvisible()
    {
        if (nextScene.Length > 0)
        {
            GameManager.LoadScene(nextScene);
            Stun(999);
        }
    }



    //freeze player
    public void SetFrozen(bool setTo)
    {
        frozen = setTo;
    }

    public int updateCoins(int amt)
    {
        playerTotalCoins += amt;
        return playerTotalCoins;
    }
}

