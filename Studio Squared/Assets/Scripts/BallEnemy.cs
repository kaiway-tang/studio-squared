using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemy : MobileEntity
{
    [SerializeField] int trackingRange;
    [SerializeField] float leapDuration;
    [SerializeField] Attack attack;
    [SerializeField] SpriteRenderer bladesRenderer;
    [SerializeField] Sprite[] blades;
    [SerializeField] Transform bladesTrfm;
    int attackCooldown, attackTimer;

    [SerializeField] EnemyHelpers helper;

    bool isActive;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        attackCooldown = Random.Range(70, 130);
    }

    [SerializeField] int rotSpd;
    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (attackCooldown > 0)
        {
            if (isActive)
            {
                attackCooldown--;

                if (attackCooldown == 50)
                {
                    GameManager.TelegraphPooler.Instantiate(trfm.position + Vector3.up);
                }
            }
        }
        else
        {
            Leap();
            attackCooldown = Random.Range(70, 130);
        }

        if (attackTimer > 0)
        {
            if (attackTimer < 21)
            {
                if (attackTimer == 20)
                {
                    attack.Activate(-1, 20);
                    bladesRenderer.sprite = blades[1];
                }
                if (attackTimer == 1)
                {
                    bladesRenderer.sprite = blades[0];
                }

                bladesTrfm.Rotate(Vector3.forward * rotSpd);

            }

            attackTimer--;
        }
        else
        {
            ApplyXFriction(.3f);
        }

        every2 = !every2;
        if (every2) { EveryTwo(); }
    }

    bool every2;
    void EveryTwo()
    {
        isActive = helper.IsActive(trackingRange);
    }

    void Leap()
    {
        attackTimer = 40;
        SetXVelocity((Player.GetPredictedPosition(leapDuration).x - trfm.position.x)/leapDuration);
        SetYVelocity(leapDuration * rb.gravityScale * 5);
    }

    public override int TakeDamage(int amount, Vector2 knockback, EntityType entitySource = EntityType.Neutral, int attackID = 0)
    {
        int result = base.TakeDamage(amount, knockback, entitySource, attackID);
        helper.FlashWhite();
        return result;
    }
}
