using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Hitbox
{
    [SerializeField] Vector2[] knockbackDirections; //predetermined knockback directions
    [SerializeField] Transform source; //only used when knockback type is 'away from source'
    [SerializeField] float knockbackPower; // ^^
    [SerializeField] int traumaAmount;
    [SerializeField] bool useSlashFX;
    const int RIGHT = 0, LEFT = 1;
    int knockbackIndex;

    protected void Start()
    {
        if (!source) { source = transform; }
    }

    public void Activate(int knockbackDirection = 0, int p_duration = 0) //-1 for knockbackDirection will deal knockback 'away from source', non -1 values apply a predetermined knockback direction
    {
        knockbackIndex = knockbackDirection;
        base.Activate(p_duration);
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected new void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer > 10 && col.gameObject.layer < 14)
        {
            if (knockbackIndex == -1)
            {
                knockbackIndex = 0;
                knockbackDirections[0] = (col.transform.position - source.position).normalized * knockbackPower;
            }
            takeDamageResult = col.GetComponent<HPEntity>().TakeDamage(damage, knockbackDirections[knockbackIndex], entityType, attackID);
            if (takeDamageResult != HPEntity.IGNORED)
            {
                CameraController.SetTrauma(traumaAmount);

                if (useSlashFX)
                {
                    GameManager.SlashFXPooler.Instantiate(col.transform.position, Random.Range(0,360));
                }
            }
            return;
        }
        else
        {
            
        }

        takeDamageResult = HPEntity.IGNORED;
    }
}
