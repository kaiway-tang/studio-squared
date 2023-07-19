using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileEntity : HPEntity
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected TerrainTrigger[] terrainTriggers;
    [SerializeField] Transform reflectionTrfm;
    [SerializeField] float knockbackFactor = 1;

    bool facingDirection;
    protected const bool RIGHT = false, LEFT = true;

    static Vector2 vect2; //cache a vector2 variable to avoid calling "new"
    static Vector3 vect3; // ^
    
    protected new void Start()
    {
        base.Start();

        if (!reflectionTrfm) { reflectionTrfm = trfm; }
    }

    protected void FlipFacing()
    {
        SetFacing(!facingDirection);
    }

    protected void SetFacing(bool direction)
    {
        if (direction != facingDirection)
        {
            vect3 = reflectionTrfm.localScale;
            vect3.x *= -1;

            reflectionTrfm.localScale = vect3;

            facingDirection = direction;
        }
    }
    protected bool IsFacingRight()
    {
        return !facingDirection;
    }
    protected bool IsFacingLeft()
    {
        return facingDirection;
    }


    void SetXVelocity(float value)
    {
        vect2.x = value;
        vect2.y = rb.velocity.y;

        rb.velocity = vect2;
    }
    void AddXVelocity(float amount)
    {
        vect2.x = amount;
        vect2.y = 0;
        rb.velocity += vect2;
    }
    protected bool AddXVelocity(float amount, float max)
    {
        if (amount > 0)
        {
            if (rb.velocity.x > max) { return false; }
            if (rb.velocity.x + amount > max)
            {
                SetXVelocity(max);
            }
            else
            {
                AddXVelocity(amount);
            }
        }
        else
        {
            if (rb.velocity.x < max) { return false; }
            if (rb.velocity.x + amount < max)
            {
                SetXVelocity(max);
            }
            else
            {
                AddXVelocity(amount);
            }
        }

        return true;
    }
    protected void AddForwardXVelocity(float amount, float max)
    {
        if (IsFacingLeft()) { amount *= -1; max *= -1; }
        AddXVelocity(amount, max);
    }

    protected void SetYVelocity(float value)
    {
        vect2.x = rb.velocity.x;
        vect2.y = value;

        rb.velocity = vect2;
    }
    protected void AddYVelocity(float amount)
    {
        vect2.x = 0;
        vect2.y = amount;

        rb.velocity += vect2;
    }
    protected void AddYVelocity(float amount, float max)
    {
        if (amount > 0)
        {
            if (rb.velocity.y > max) { return; }
            if (rb.velocity.y + amount > max)
            {
                SetYVelocity(max);
            }
            else
            {
                AddYVelocity(amount);
            }
        }
        else
        {
            if (rb.velocity.y < max) { return; }
            if (rb.velocity.y + amount < max)
            {
                SetYVelocity(max);
            }
            else
            {
                AddYVelocity(amount);
            }
        }
    }


    protected void ApplyXFriction(float amount)
    {
        if (Mathf.Abs(rb.velocity.x) < amount)
        {
            SetXVelocity(0);
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                AddXVelocity(-amount);
            }
            else
            {
                AddXVelocity(amount);
            }
        }
    }

    protected bool IsOnGround()
    {
        return terrainTriggers[0].isTouching > 0;
    }

    protected bool TerrainTriggerTouching(int index)
    {
        return terrainTriggers[index].isTouching > 0;
    }


    public override int TakeDamage(int amount, Vector2 knockback, EntityType entitySource = EntityType.Neutral, int attackID = 0)
    {
        int result = base.TakeDamage(amount, knockback, entitySource, attackID);

        if (result == ALIVE)
        {
            rb.velocity = knockback * knockbackFactor;
        }

        return result;
    }
}