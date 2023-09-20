using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    float fadeRate, fadeTarget;
    int mode;
    public const int STATIC = 0, FADING_IN = 1, FADE_OUT = 2;
    public void FadeIn(float rate, float target = 1, bool overrider = false)
    {
        fadeRate = rate;
        mode = FADING_IN;

        if (overrider && mode == FADING_IN)
        {
            if (fadeTarget < target) { fadeTarget = target; }
            return; 
        }
        fadeTarget = target; 
    }

    public void FadeOut(float rate, float target = 0)
    {
        fadeRate = rate;
        fadeTarget = target;
        mode = FADE_OUT;
    }

    public void SetAlpha(float value)
    {
        Toolbox.AddAlpha(spriteRenderer, value - spriteRenderer.color.a);
    }

    public int GetStatus()
    {
        return mode;
    }

    private void OnDisable()
    {
        mode = STATIC;
    }

    private void FixedUpdate()
    {
        if (mode == FADING_IN)
        {
            if (Toolbox.AddAlpha(spriteRenderer, fadeRate) >= fadeTarget)
            {
                mode = STATIC;
                FadeInComplete();
            }
        }
        else if (mode == FADE_OUT)
        {
            if (Toolbox.AddAlpha(spriteRenderer, -fadeRate) <= fadeTarget)
            {
                mode = STATIC;
                FadeOutComplete();
            }
        }
    }

    protected virtual void FadeOutComplete()
    {

    }
    protected virtual void FadeInComplete()
    {

    }
}
