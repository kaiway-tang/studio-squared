using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveInManager : MonoBehaviour
{
    [SerializeField] Transform tntMinecart, fuse;
    [SerializeField] ParticleSystem fuseSparks;
    [SerializeField] Fader interactKeyFader;
    [SerializeField] GameObject collapsingSystem;
    Transform trfm;

    bool playerClose, collapsing, cartBlown;
    int cutSceneOneTimer, cutSceneTwoTimer;

    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
    }

    void Update()
    {
        if (PlayerInput.InteractPressed() && Toolbox.InBoxRange(GameManager.playerTrfm.position, trfm.position, 1.5f))
        {
            Interact();
        }
    }

    private void FixedUpdate()
    {
        if (collapsing)
        {
            CameraController.SetTrauma(5);
        }

        if (playerClose != Toolbox.InBoxRange(GameManager.playerTrfm.position, trfm.position, 1.5f))
        {
            playerClose = Toolbox.InBoxRange(GameManager.playerTrfm.position, trfm.position, 1.5f);

            if (playerClose) { interactKeyFader.FadeIn(.05f); }
            else { interactKeyFader.FadeOut(.05f); }
        }

        if (cutSceneTwoTimer > 0)
        {
            cutSceneTwoTimer++;
            if (cutSceneTwoTimer == 50)
            {
                collapsingSystem.SetActive(true);
            }
        }

        if (cutSceneOneTimer > 0)
        {
            cutSceneOneTimer--;
            if (cutSceneOneTimer == 40)
            {
                fuseSparks.Play();
            }
        }
    }

    void Interact()
    {
        if (tntMinecart.position.x > 676)
        {
            if (!cartBlown)
            {
                Player.self.Stun(50);
                CameraController.SetTrauma(40);
                HUDManager.WhiteFlash(.5f);
                cartBlown = true;
                collapsing = true;
                Destroy(tntMinecart.gameObject);
                cutSceneTwoTimer = 1;
            }
        }
        else
        {
            cutSceneOneTimer = 150;
            CameraController.QueCameraPan(new Vector2(600, 180), 25, 20);
            CameraController.QueCameraPan(fuse.position + Vector3.up * 2, 150, 10);
            CameraController.QueCameraPan(tntMinecart.position + Vector3.right * 4 + Vector3.up * 3, 100, 0);
        }
    }
    
}
