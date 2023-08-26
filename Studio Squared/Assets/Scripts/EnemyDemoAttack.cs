using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDemoAttack : Attack
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color fade;

    public new void Activate(int p_direction, int p_duration)
    {
        base.Activate(p_direction, p_duration);
        spriteRenderer.color = Color.red;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color -= fade;
        }
    }

    new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
