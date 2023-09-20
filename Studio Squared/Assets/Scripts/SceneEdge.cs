using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEdge : CameraStop
{
    public string nextScene;
    [SerializeField] Vector3 spawnPosition;

    new void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == GameManager.PlayerTriggerLayer)
        {
            TriggerEnteredByPlayer();
            GameManager.spawnPosition = spawnPosition;
            Player.nextScene = nextScene;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 1)
        {
            TriggerExitedByPlayer();
            Player.nextScene = "";
        }
    }
}
