using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected HPEntity.EntityType entityType;
    public Transform trfm;
    [SerializeField] Collider2D hitbox;

    protected int takeDamageResult, attackID;

    static int attackIDDistributor;

    public void Activate(bool staticHitbox = false) //static hitboxes can damage opponents multiple times if they exit and re-enter the hitbox
    {
        hitbox.enabled = true;

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

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 9)
        {
            takeDamageResult = col.GetComponent<HPEntity>().TakeDamage(damage, entityType, attackID);   
            return;
        }
        takeDamageResult = HPEntity.IGNORED;
    }
}
