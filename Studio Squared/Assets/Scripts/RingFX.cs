using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFX : MonoBehaviour
{
    [SerializeField] Color fadeRate;
    [SerializeField] Vector2 scaleRate;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform trfm;
    [SerializeField] int life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spriteRenderer.color -= fadeRate;
        trfm.localScale *= scaleRate;
        life--;

        if (life < 1)
        {
            Destroy(gameObject);
        }
    }
}
