using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFX : PooledObject
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Transform parent;
    int index, timer;
    // Start is called before the first frame update
    new void OnEnable()
    {
        base.OnEnable();
        index = Random.Range(0,3);
        spriteRenderer.sprite = sprites[index];
        timer = 0;
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        timer++;

        if (timer % 3 == 0)
        {
            index++;
            if (index > 2) { index = 0; }
            spriteRenderer.sprite = sprites[index];
        }

        base.FixedUpdate();
    }

    public override void Destantiate()
    {
        trfm.parent = null;
        base.Destantiate();
    }
}
