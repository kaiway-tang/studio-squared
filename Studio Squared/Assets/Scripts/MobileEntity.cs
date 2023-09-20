using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileEntity : HPEntity
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] protected TerrainTrigger[] terrainTriggers;
    [SerializeField] Transform reflectionTrfm;
    [SerializeField] float knockbackFactor = 1;

    bool facingDirection;
    public const bool RIGHT = false, LEFT = true;
    public int stunned;

    static Vector2 vect2; //cache a vector2 variable to avoid calling "new"
    static Vector3 vect3; // ^
    
    protected new void Start()
    {
        base.Start();

        if (!reflectionTrfm) { reflectionTrfm = trfm; }
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
        if (stunned > 0) { stunned--; }
    }

    protected void FlipFacing()
    {
        SetFacing(!facingDirection);
    }

    public bool SetFacing(bool direction) //returns true if facing direction changed
    {
        if (direction != facingDirection)
        {
            vect3 = reflectionTrfm.localScale;
            vect3.x *= -1;

            reflectionTrfm.localScale = vect3;

            facingDirection = direction;

            return true;
        }
        return false;
    }
    protected bool IsFacingRight()
    {
        return !facingDirection;
    }
    protected bool IsFacingLeft()
    {
        return facingDirection;
    }


    public void SetXVelocity(float value)
    {
        vect2.x = value;
        vect2.y = rb.velocity.y;

        rb.velocity = vect2;
    }
    public void AddXVelocity(float amount)
    {
        vect2.x = amount;
        vect2.y = 0;
        rb.velocity += vect2;
    }
    public bool AddXVelocity(float amount, float max)
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
    public void AddForwardXVelocity(float amount, float max)
    {
        if (IsFacingLeft()) { amount *= -1; max *= -1; }
        AddXVelocity(amount, max);
    }

    public void AddBackwardXVelocity(float amount, float max)
    {
        if (IsFacingRight()) { amount *= -1; max *= -1; }
        AddXVelocity(amount, max);
    }

    public void SetYVelocity(float value)
    {
        vect2.x = rb.velocity.x;
        vect2.y = value;

        rb.velocity = vect2;
    }
    public void AddYVelocity(float amount)
    {
        vect2.x = 0;
        vect2.y = amount;

        rb.velocity += vect2;
    }
    public void AddYVelocity(float amount, float max)
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


    public void ApplyXFriction(float amount)
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

    float magnitude, ratio;
    public void ApplyDirectionalFriction(float amount)
    {
        if (Mathf.Abs(rb.velocity.x) > 0.0001f || Mathf.Abs(rb.velocity.y) > 0.0001f)
        {
            vect2.x = rb.velocity.x;
            vect2.y = rb.velocity.y;
            magnitude = vect2.magnitude;
            ratio = (magnitude - amount) / magnitude;

            if (ratio > 0)
            {
                vect2.x = rb.velocity.x * ratio;
                vect2.y = rb.velocity.y * ratio;

                rb.velocity = vect2;
            }
            else
            {
                vect2.x = 0;
                vect2.y = 0;

                rb.velocity = vect2;
            }
        }
        else
        {
            vect2.x = 0;
            vect2.y = 0;

            rb.velocity = vect2;
        }
    }
    public bool IsOnGround()
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
            TakeKnockback(knockback);
        }

        return result;
    }

    public void TakeKnockback(Vector2 knockback)
    {
        rb.velocity = knockback * knockbackFactor;
    }

    public void Stun(int duration)
    {
        if (stunned < duration) { stunned = duration; }
    }

    public void AddVelocity(Vector2 velocity, float max)
    {
        AddXVelocity(velocity.x, velocity.normalized.x * max);
        AddYVelocity(velocity.y, velocity.normalized.y * max);
    }
}