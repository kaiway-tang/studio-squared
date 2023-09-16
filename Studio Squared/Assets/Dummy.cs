using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MobileEntity
{
    [SerializeField] float returnForce, distanceThreshold, friction;
    [SerializeField] EnemyHelpers helper;
    [SerializeField] Sprite[] sprites;

    int hurtTimer;
    Vector2 startingPos;
    Rigidbody2D rb;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        trfm = transform;
        startingPos = trfm.position;
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (Toolbox.InBoxRange(trfm.position, startingPos, distanceThreshold))
        {
            
        }
        else
        {
            rb.velocity += (startingPos - (Vector2)trfm.position).normalized * returnForce;
        }
        ApplyDirectionalFriction(friction);

        if (hurtTimer > 0)
        {
            hurtTimer--;
            if (hurtTimer == 0)
            {
                helper.spriteRenderer[0].sprite = sprites[0];
            }
        }
    }

    public override int TakeDamage(int amount, Vector2 knockback, EntityType entitySource = EntityType.Neutral, int attackID = 0)
    {
        int result = base.TakeDamage(amount, knockback, entitySource, attackID);

        hurtTimer = 25;
        helper.spriteRenderer[0].sprite = sprites[1];

        helper.FlashWhite();
        return result;
    }
}
