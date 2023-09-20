using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int use;
    [SerializeField] Sprite[] unlockSprites;
    public const int DJUMP = 0, DASH = 1, WALL_JUMP = 2;
    [SerializeField] bool gameCut;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Fader interactKeyFader;
    int timer;
    bool playerClose;
    Transform trfm;
    void Start()
    {
        trfm = transform;
    }

    private void Update()
    {
        if (playerClose && PlayerInput.InteractPressed())
        {
            if (timer == 100)
            {
                GrantPickup();
                HUDManager.FadeBlackCoverOpacity(0);
            }
            else if (timer == 0)
            {
                Interact();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0 && timer != 100)
        {
            timer++;

            if (timer == 50)
            {
                if (TutorialManager.usingArrows && unlockSprites.Length > 1)
                {
                    UnlockUI.ShowText(unlockSprites[1]);
                }
                else
                {
                    UnlockUI.ShowText(unlockSprites[0]);
                }
            }

            if (timer > 199)
            {
                if (Toolbox.AddAlpha(spriteRenderer, -0.05f) <= 0 ) { Destroy(gameObject); }
            }
        }

        if (!disappearing)
        {
            DoBreatheAnimation();
        }


        if (playerClose != Toolbox.InBoxRange(GameManager.playerTrfm.position, trfm.position, 1.5f))
        {
            playerClose = Toolbox.InBoxRange(GameManager.playerTrfm.position, trfm.position, 1.5f);

            if (playerClose) { interactKeyFader.FadeIn(.05f); }
            else { interactKeyFader.FadeOut(.05f); }
        }
    }

    bool breatheIn, disappearing;
    void DoBreatheAnimation()
    {
        if (breatheIn)
        {
            if (Toolbox.AddAlpha(spriteRenderer, 0.007f) >= 1)
            {
                breatheIn = false;  
            }
        }
        else
        {
            if (Toolbox.AddAlpha(spriteRenderer, -0.007f) <= 0.6f)
            {
                breatheIn = true;
            }
        }
    }

    public void Interact()
    {
        if (gameCut)
        {
            HUDManager.FadeBlackCoverOpacity(.8f);
            timer = 1;
            Player.self.SetFrozen(true);
        }
        else
        {
            GrantPickup();
        }

        interactKeyFader.FadeOut(.05f);
    }

    void GrantPickup()
    {
        if (use == DJUMP)
        {
            Player.hasDoubleJump = true;
        }
        else if (use == DASH)
        {
            Player.hasDash = true;
        }
        else if (use == WALL_JUMP)
        {
            Player.hasWallJump = true;
        }

        Disappear();
    }

    void Disappear()
    {
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
        UnlockUI.ClearText();

        disappearing = true;
        timer = 200;

        if (gameCut) { Player.self.SetFrozen(false); }
    }
}
