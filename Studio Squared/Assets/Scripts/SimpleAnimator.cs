using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimator : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int ticksPerFrame;
    int timer, currentSprite;
    // Start is called before the first frame update
    void Start()
    {
        currentSprite = 999;
    }

    private void FixedUpdate()
    {
        if (currentSprite < sprites.Length)
        {
            if (timer > 0)
            {
                timer--;
                if (timer <= 0)
                {
                    timer = ticksPerFrame;
                    currentSprite++;
                    if (currentSprite == sprites.Length)
                    {
                        spriteRenderer.sprite = null;
                    }
                    else
                    {
                        spriteRenderer.sprite = sprites[currentSprite];
                    }
                }
            }
        }
    }

    public void Play()
    {
        currentSprite = 0;
        spriteRenderer.sprite = sprites[0];
        timer = ticksPerFrame;
    }
}
