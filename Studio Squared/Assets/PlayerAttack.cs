using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
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
                Player.self.TakeKnockback(Vector2.up * 6);
                Player.self.AddForwardXVelocity(12,12);
            }
            Player.AddMana(10);
            firstStrike = true;
        }
    }
}
