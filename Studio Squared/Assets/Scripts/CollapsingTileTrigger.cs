using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingTileTrigger : MonoBehaviour
{
    [SerializeField] CollapsingTile tile;
    [SerializeField] ParticleSystem ptclSys;
    [SerializeField] Fader fogFader;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == GameManager.PlayerCollisionLayer)
        {
            CameraController.AddTrauma(18);
            ptclSys.Play();
            Player.LockMovement(true);
            Invoke("Collapse", .4f);
        }
    }

    void Collapse()
    {
        Player.LockMovement(false);
        CameraController.AddTrauma(18);
        tile.Collapse();
        fogFader.FadeOut(.05f);
    }
}
