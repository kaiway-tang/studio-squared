using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Hitbox
{
    [SerializeField] Vector2[] knockbacks;
    [SerializeField] int traumaAmount;
    const int RIGHT = 0, LEFT = 1;
    int knockbackIndex;

    // Start is called before the first frame update
    public void Activate(int knockbackDirection = 0, int p_duration = 0)
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
        Debug.Log("hit: " + col.gameObject);

        if (col.gameObject.layer == 9)
        {
            takeDamageResult = col.GetComponent<HPEntity>().TakeDamage(damage, knockbacks[knockbackIndex], entityType, attackID);
            CameraController.SetTrauma(traumaAmount);
            return;
        }
        takeDamageResult = HPEntity.IGNORED;
    }
}
