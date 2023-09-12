using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    float fadeRate, fadeTarget;
    int mode;
    public const int STATIC = 0, FADING_IN = 1, FADE_OUT = 2;
    public void FadeIn(float rate, float target = 1)
    {
        fadeRate = rate;
        fadeTarget = target; 
        mode = FADING_IN;
    }

    public void FadeOut(float rate, float target = 0)
    {
        fadeRate = rate;
        fadeTarget = target;
        mode = FADE_OUT;
    }

    public int GetStatus()
    {
        return mode;
    }

    private void FixedUpdate()
    {
        if (mode == FADING_IN)
        {
            if (Toolbox.AddAlpha(spriteRenderer, fadeRate) >= fadeTarget)
            {
                mode = STATIC;
            }
        }
        else if (mode == FADE_OUT)
        {
            if (Toolbox.AddAlpha(spriteRenderer, -fadeRate) <= fadeTarget)
            {
                mode = STATIC;
            }
        }
    }
}
