using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] Fader fader;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == GameManager.PlayerCollisionLayer)
        {
            //fader.FadeOut(.05f);
        }
    }
}
