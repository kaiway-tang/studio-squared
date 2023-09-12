using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage, knockback;
    [SerializeField] protected float speed;
    [SerializeField] protected HPEntity.EntityType entityType;
    [SerializeField] protected Transform trfm;
    [SerializeField] GameObject DestroyFX;
    [SerializeField] int life;
    // Start is called before the first frame update
    protected void Start()
    {

    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        trfm.position += trfm.up * speed;

        life--;
        if (life == 0) { End(); }
    }

    protected void End()
    {
        Instantiate(DestroyFX, trfm.position, trfm.rotation);
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer > 10 && col.gameObject.layer < 14)
        {
            if (col.GetComponent<HPEntity>().TakeDamage(damage, trfm.up * knockback, entityType) != HPEntity.IGNORED)
            {
                End();
            }
        }
        else
        {
            End();
        }
    }
}