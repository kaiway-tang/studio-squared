using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    [SerializeField] float maxStickingVelocity;
    bool firstStrike;

    public new void Activate(int knockbackDirection = 0, int p_duration = 0) //-1 for knockbackDirection will deal knockback 'away from source', non -1 values apply a predetermined knockback direction
    {
        firstStrike = false;
        base.Activate(knockbackDirection, p_duration);
    }

    protected override void EntityHit(Collider2D col, int takeDamageResult)
    {
        if (!firstStrike)
        {
            if (!Player.self.IsOnGround())
            {
                MobileEntity mobEnt;
                if (mobEnt = col.gameObject.GetComponent<MobileEntity>())
                {
                    float magnitude = mobEnt.rb.velocity.magnitude;
                    if (magnitude > maxStickingVelocity)
                    {
                        Player.self.rb.velocity = mobEnt.rb.velocity / magnitude * maxStickingVelocity;
                    }
                    else
                    {
                        Player.self.rb.velocity = mobEnt.rb.velocity;
                    }
                }

                Player.self.AddYVelocity(7, 7 + Player.self.rb.velocity.y);
            }
            Player.AddMana(10);
            firstStrike = true;
        }
    }
}
