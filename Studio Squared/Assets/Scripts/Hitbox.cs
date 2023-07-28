using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected HPEntity.EntityType entityType;
    [SerializeField] Collider2D hitbox;

    protected int takeDamageResult, attackID, duration;

    static int attackIDDistributor;

    public void Activate(int p_duration = 0, bool staticHitbox = false) //static hitboxes can damage opponents multiple times if they exit and re-enter the hitbox
    {
        hitbox.enabled = true;
        duration = p_duration;

        if (staticHitbox)
        {
            attackID = 0;
        }
        else
        {
            attackIDDistributor++;
            attackID = attackIDDistributor;
        }
    }

    public void Deactivate()
    {
        hitbox.enabled = false;
    }

    protected void FixedUpdate()
    {
        if (duration > 0)
        {
            if (duration == 1)
            {
                Deactivate();
            }
            duration--;
        }
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 9)
        {
            takeDamageResult = col.GetComponent<HPEntity>().TakeDamage(damage, Vector2.zero, entityType, attackID);   
            return;
        }
        takeDamageResult = HPEntity.IGNORED;
    }
}
