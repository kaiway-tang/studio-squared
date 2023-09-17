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
        if (col.gameObject.layer == GameManager.PlayerTriggerLayer)
        {
            CameraController.AddTrauma(18);
            ptclSys.Play();
            Player.LockMovement(20);
            Invoke("Collapse", .4f);
        }
    }

    void Collapse()
    {
        CameraController.AddTrauma(18);
        tile.Collapse();
        fogFader.FadeOut(.05f);
    }
}
