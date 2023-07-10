using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileEntity : HPEntity
{
    [SerializeField] protected Rigidbody2D rb;
    bool facingDirection;
    const bool RIGHT = false, LEFT = true;

    Vector2 vect2; //cache a vector2 variable to avoid calling "new"
    
    protected new void Start()
    {
        base.Start();
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

    protected void AddXVelocity(float amount, float max)
    {
        if (amount > 0)
        {
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
            if (rb.velocity.x + amount < max)
            {
                SetXVelocity(max);
            }
            else
            {
                AddXVelocity(amount);
            }
        }
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
}