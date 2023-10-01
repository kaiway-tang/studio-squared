using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveInManager : MonoBehaviour
{
    [SerializeField] Transform tntMinecart, fuse, hpGem, pump;
    [SerializeField] ParticleSystem fuseSparks, rockExplosion;
    [SerializeField] Fader interactKeyFader;
    [SerializeField] GameObject collapsingSystem, sceneEdge;
    Transform trfm;

    bool playerClose, collapsing, cartBlown;
    int cutSceneOneTimer, cutSceneTwoTimer;
    static Vector2 tntMinecartPosition;

    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
        if (tntMinecartPosition.x > 676)
        {
            tntMinecart.position = tntMinecartPosition;
        }
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
            CameraController.SetTrauma(7);
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
            if (cutSceneTwoTimer == 25)
            {
                Player.self.Stun(175);
                CameraController.SetTrauma(38);
                HUDManager.WhiteFlash(.7f);
                collapsing = true;
                Destroy(tntMinecart.gameObject);
                sceneEdge.SetActive(true);
            }
            if (cutSceneTwoTimer == 50)
            {
                pump.localPosition = new Vector3(0, .3f, 0);
                rockExplosion.Play();
            }
            if (cutSceneTwoTimer == 100)
            {
                collapsingSystem.SetActive(true);
                CameraController.QueCameraPan(hpGem.position, 125, 30);
            }
            if (cutSceneTwoTimer == 275)
            {
                collapsingSystem.GetComponent<CaveCollapse>().speedUncapped = true;
            }
        }

        if (cutSceneOneTimer > 0)
        {
            cutSceneOneTimer--;
            if (cutSceneOneTimer == 40)
            {
                pump.localPosition = new Vector3(0, .9f, 0);
                fuseSparks.Play();
            }
        }
    }

    void Interact()
    {
        if (tntMinecart.position.x > 676)
        {
            tntMinecartPosition = tntMinecart.position;
            if (!cartBlown)
            {
                cartBlown = true;
                cutSceneTwoTimer = 1;
            }
        }
        else
        {
            cutSceneOneTimer = 150;
            CameraController.QueCameraPan(new Vector2(600, 180), 25, 20);
            CameraController.QueCameraPan(fuse.position + Vector3.up * 2, 150, 10);
            CameraController.QueCameraPan(tntMinecart.position + Vector3.right * 3 + Vector3.up * 2, 100, 0);
        }

        pump.localPosition = new Vector3(0, .3f, 0);
    }
    
}
