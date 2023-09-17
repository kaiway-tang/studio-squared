 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltProjectile : Attack
{
    [SerializeField] float speed;
    [SerializeField] int life;
    [SerializeField] Transform trfm, parent;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        trfm.position += trfm.right * speed;

        life--;
        if (life < 1) { Destroy(parent.gameObject); }
    }

    protected override void EntityHit(Collider2D col, int takeDamageResult)
    {
        if (takeDamageResult != HPEntity.IGNORED)
        {
            if (takeDamageResult == HPEntity.ALIVE)
            {
                //GameManager.LightningFXPooler.Instantiate(col.transform.position + Vector3.up * 6).parent = col.transform;
            }
            else if (takeDamageResult == HPEntity.DEAD)
            {
                //GameManager.LightningFXPooler.Instantiate(col.transform.position + Vector3.up * 6);
            }
            GameManager.LightningPtclsPooler.Instantiate(col.transform.position);
        }
    }
}
