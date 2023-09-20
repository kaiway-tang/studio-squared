using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPing : MonoBehaviour
{
    [SerializeField] Transform ringTrfm;
    [SerializeField] Vector3 scale;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color fade;
    int timer;

    void FixedUpdate()
    {
        if (timer > 50)
        {
            ringTrfm.localScale += scale;
            spriteRenderer.color -= fade;
            if (timer > 100)
            {
                ringTrfm.localScale = Vector3.zero;
                spriteRenderer.color = new Color(1,1,0.5f,1);
                timer = 0;
            }
        }

        timer++;
    }
}
