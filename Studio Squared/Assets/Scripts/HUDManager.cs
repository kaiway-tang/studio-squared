using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Transform swordHUDTrfm;
    [SerializeField] GameObject castPrompt;
    [SerializeField] SpriteRenderer castPromptRenderer, darkBackgroundRenderer;
    [SerializeField] Sprite castPromptAlt;
    bool castPromptActive;

    public float screenXSize, screenYSize, lastScreenXSize, lastScreenYSize;

    public SpriteRenderer vignetteRenderer, vignetteBoostRenderer, blackCoverSpriteRenderer, manaFlashRenderer;

    float blackCoverTargetAlpha, darkBackgroundTargetAlpha;
    Color fadeRate = new Color(0, 0, 0, 0.01f);
    Color color;

    int alignHUDTimer;

    public static HUDManager self;
    private void Awake()
    {
        self = GetComponent<HUDManager>();
    }

    private void Start()
    {
        SetBlackCoverOpacity(1);
        FadeBlackCoverOpacity(0);
        CalculateScreenSize();
        AlignHUDElements();
    }
    private void FixedUpdate()
    {
        ProcessFading();
        ManageHUDAlignment();

        if (castPromptActive && Player.mana < 91)
        {
            castPrompt.SetActive(false);
            castPromptActive = false;
        }
    }

    bool CalculateScreenSize()
    {
        screenYSize = 2 * Camera.main.orthographicSize;
        screenXSize = screenYSize * Camera.main.aspect;

        if (Mathf.Abs(screenXSize - lastScreenXSize) > .001f || Mathf.Abs(screenYSize - lastScreenYSize) > .001f)
        {
            lastScreenXSize = screenXSize;
            lastScreenYSize = screenYSize;
            return true;
        }
        return false;
    }

    void AlignHUDElements()
    {
        return;

        vignetteRenderer.transform.localScale = new Vector3(.1788f * screenXSize, .37f * screenYSize, 1);

        Vector3 hudPosition = swordHUDTrfm.localPosition;
        hudPosition.x = screenXSize * -.5f + 7.5f;
        swordHUDTrfm.localPosition = hudPosition;
    }

    void ManageHUDAlignment()
    {
        if (alignHUDTimer > 0) { alignHUDTimer--; }
        else
        {
            alignHUDTimer = 100;
            if (CalculateScreenSize())
            {
                AlignHUDElements();
            }
        }
    }

    bool fadingManaFlash;
    public static void FlashManaBar()
    {
        self.fadingManaFlash = true;
        self.manaFlashRenderer.color = Color.white;
    }


    public static void SetBlackCoverOpacity(float alpha)
    {
        self.color = self.blackCoverSpriteRenderer.color;
        self.color.a = alpha;
        self.blackCoverSpriteRenderer.color = self.color;
    }

    [SerializeField] bool fadingBlackCover, fadingDarkBackground;
    public static void FadeBlackCoverOpacity(float targetAlpha)
    {
        if (Mathf.Abs(self.blackCoverSpriteRenderer.color.a - targetAlpha) < .01f) { return; }

        self.fadingBlackCover = true;
        self.blackCoverTargetAlpha = targetAlpha;
    }

    public static void FadeDarkBackgroundOpacity(float targetAlpha)
    {
        if (Mathf.Abs(self.darkBackgroundRenderer.color.a - targetAlpha) < .01f) { return; }

        self.fadingDarkBackground = true;
        self.darkBackgroundTargetAlpha = targetAlpha;
    }

    void ProcessFading()
    {
        if (fadingBlackCover)
        {
            if (blackCoverSpriteRenderer.color.a - blackCoverTargetAlpha > .011f) { blackCoverSpriteRenderer.color -= fadeRate; }
            else if (blackCoverSpriteRenderer.color.a - blackCoverTargetAlpha < -.011f) { blackCoverSpriteRenderer.color += fadeRate; }
            else
            {
                blackCoverSpriteRenderer.color = new Color(0, 0, 0, blackCoverTargetAlpha);
                fadingBlackCover = false;
            }
        }

        if (fadingDarkBackground)
        {
            if (darkBackgroundRenderer.color.a - darkBackgroundTargetAlpha > .011f) { darkBackgroundRenderer.color -= fadeRate * 4; }
            else if (darkBackgroundRenderer.color.a - darkBackgroundTargetAlpha < -.011f) { darkBackgroundRenderer.color += fadeRate * 4; }
            else
            {
                darkBackgroundRenderer.color = new Color(0, 0, 0, darkBackgroundTargetAlpha);
                fadingDarkBackground = false;
            }
        }

        if (fadingVignette)
        {
            if (vignetteBoostRenderer.color.a > 0)
            {
                vignetteBoostRenderer.color -= fadeRate;
            }
            if (vignetteRenderer.color.a > 0)
            {
                vignetteRenderer.color -= fadeRate;
            }
            else
            {
                color = vignetteRenderer.color;
                color.a = 0;
                vignetteRenderer.color = color;
                vignetteBoostRenderer.color = color;
                fadingVignette = false;
            }
        }

        if (fadingManaFlash)
        {
            if (manaFlashRenderer.color.a > 0)
            {
                manaFlashRenderer.color -= fadeRate * 4;
            }
            else
            {
                color = manaFlashRenderer.color;
                color.a = 0;
                manaFlashRenderer.color = color;
                fadingManaFlash = false;
            }
        }
    }

    bool fadingVignette;
    public static void SetVignetteOpacity(float alpha)
    {
        self.fadingVignette = true;

        if (self.vignetteRenderer.color.a < alpha)
        {
            self.color = self.vignetteRenderer.color;
            self.color.a = alpha;
            self.vignetteRenderer.color = self.color;

            if (alpha > 1)
            {
                self.color.a = alpha - 1;
                self.vignetteBoostRenderer.color = self.color;
            }
        }
    }

    public static void DoCastPrompt()
    {
        /*
        if (TutorialManager.usingArrows)
        {
            self.castPromptRenderer.sprite = self.castPromptAlt;
        }
        */

        self.castPrompt.SetActive(true);
        self.castPromptActive = true;
    }
}
