using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastChargeRing : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] SpriteRenderer spriteRenderer;
    int timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;

        trfm.localScale -= Vector3.one * .15f;
        spriteRenderer.color += Color.black * .1f;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }
}
