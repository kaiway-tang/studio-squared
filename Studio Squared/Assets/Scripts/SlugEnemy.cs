using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugEnemy : MobileEntity
{
    [SerializeField] int trackingRange, spitRange, lungeRange, speed, lungePower;
    [SerializeField] int[] cooldownLengths;
    [SerializeField] SimpleAnimator crawlAnimation;
    [SerializeField] Sprite windup, lungeWindup, lunge;
    [SerializeField] GameObject spitball;
    [SerializeField] TrailRenderer attackTrailFX;
    [SerializeField] ParticleSystem spitPtcls;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Transform spriteTrfm, firepoint;
    [SerializeField] Attack attack;

    [SerializeField] float friction;
    [SerializeField] EnemyHelpers helper;

    [SerializeField] GameObject splitSlug;

    int selectedAttack, timer, attackCooldown;
    const int NONE = 0, LUNGE = 1, SPIT = 2;

    Quaternion spitBallAim;
    bool everyTwo;
    bool isActive;

    new void Start()
    {
        timer = Random.Range(10, 14);
        base.Start();
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (stunned > 0) return;

        if (everyTwo) { EveryTwo(); }
        everyTwo = !everyTwo;
    }

    bool frictionOn;

    void EveryTwo()
    {
        if (frictionOn)
        {
            ApplyXFriction(friction);
        }

        if (helper.IsActive(trackingRange) && timer < 1)
        {
            if (attackCooldown < 1 && helper.InBoxRangeToPlayer(spitRange))
            {
                if (Mathf.Abs(GameManager.playerTrfm.position.x - trfm.position.x) < lungeRange
                    && Mathf.Abs(trfm.position.y - GameManager.playerTrfm.position.y - 1) < 2
                    && Random.Range(0,2) == 0)
                {
                    selectedAttack = LUNGE;
                    timer = 20;
                    PrepareAttack();
                    spriteRenderer.sprite = lungeWindup;
                    attackCooldown = Random.Range(cooldownLengths[0], cooldownLengths[1]); //10, 16
                }
                else
                {
                    selectedAttack = SPIT;
                    PrepareAttack();
                    spriteRenderer.sprite = windup;
                    spitPtcls.Play();
                    attackCooldown = Random.Range(cooldownLengths[0] * 2, cooldownLengths[1] * 2); //30, 41
                    timer = 15;
                }
            }
            else
            {
                timer = Random.Range(11, 14);
                crawlAnimation.Play();
                frictionOn = true;
            }
            helper.FacePlayer();
        }
        else
        {
            if (selectedAttack == LUNGE)
            {
                if (timer == 12)
                {
                    attackTrailFX.emitting = true;
                    spriteRenderer.sprite = lunge;
                    AddForwardXVelocity(lungePower, 30);
                    AddYVelocity(lungePower);
                    frictionOn = false;
                }
                if (timer == 9)
                {
                    attack.Activate();
                }
                if (timer == 1)
                {
                    attack.Deactivate();
                    attackTrailFX.emitting = false;
                    selectedAttack = NONE;
                    frictionOn = true;
                }
            }
            else if (selectedAttack == SPIT)
            {
                if (timer == 11)
                {
                    spitBallAim = helper.GetQuaternionToPlayerHead(firepoint.position);
                    helper.FacePlayer();
                }
                if (timer == 7)
                {
                    spriteRenderer.sprite = lunge;
                    Instantiate(spitball, firepoint.position, spitBallAim);
                    AddForwardXVelocity(-16, -16);
                }
                if (timer == 1)
                {
                    selectedAttack = NONE;
                }
            }
            else if (selectedAttack == NONE)
            {
                if (timer == 5)
                {
                    AddForwardXVelocity(speed, speed);
                    frictionOn = false;
                }
            }

            if (rb.velocity.y < -18) { SetYVelocity(-18); }
            timer--;
        }

        if (attackCooldown > 0) { attackCooldown--; }
    }

    public void InitiateSpawnObject(Vector2 spawnVelocity, int invalidAttackID)
    {
        SetInvalidAttackID(invalidAttackID);
        SetInvulnerable(10);
        rb.velocity = spawnVelocity;
    }







    void PrepareAttack()
    {
        GameManager.TelegraphPooler.Instantiate(spriteTrfm.position + spriteTrfm.right);
    }

    protected override void OnDamageTaken(int amount, int result)
    {
        if (result == HPEntity.DEAD && splitSlug)
        {
            Instantiate(splitSlug, trfm.position, trfm.rotation).GetComponent<SlugEnemy>().InitiateSpawnObject((Vector3.up + Vector3.right) * 17, trackedAttackIDs[latestAttackIDIndex]);
            Instantiate(splitSlug, trfm.position, trfm.rotation).GetComponent<SlugEnemy>().InitiateSpawnObject((Vector3.up + Vector3.left) * 17, trackedAttackIDs[latestAttackIDIndex]);
        }
        else
        {
            helper.FlashWhite();
        }
    }
}
