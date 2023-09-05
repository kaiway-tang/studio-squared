using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingWall : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] spriteRenderer;
    Color fade;
    [SerializeField] float fadeRate;
    int mode;
    const int STATIC = 0, FADE_IN = 1, FADE_OUT = 2;
    void Start()
    {
        fade = new Color(0, 0, 0, .01f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mode == FADE_OUT)
        {
            if (spriteRenderer[0].color.a > 0)
            {
                AddAlpha(-fadeRate);
            }
            else
            {

                SetAlpha(0);
                mode = STATIC;
            }
        }
        else if (mode == FADE_IN)
        {
            if (spriteRenderer[0].color.a < 1)
            {
                AddAlpha(fadeRate);
            }
            else
            {
                SetAlpha(1);
                mode = STATIC;
            }
        }
    }

    void AddAlpha(float alpha)
    {
        for (int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].color += fade * alpha;
        }
    }

    void SetAlpha(float alpha)
    {
        for (int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].color = new Color(spriteRenderer[i].color.r, spriteRenderer[i].color.g, spriteRenderer[i].color.b, alpha);
        }
    }

    void FadeIn()
    {
        mode = FADE_IN;
        HUDManager.FadeDarkBackgroundOpacity(0);
    }

    void FadeOut()
    {
        mode = FADE_OUT;
        HUDManager.FadeDarkBackgroundOpacity(1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        FadeOut();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        FadeIn();
    }
}
