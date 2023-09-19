using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] Fader fader;
    [SerializeField] bool permanent;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == GameManager.PlayerTriggerLayer)
        {
            fader.FadeOut(.05f);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (permanent && col.gameObject.layer == GameManager.PlayerTriggerLayer)
        {
            fader.FadeIn(.05f);
        }
    }
}
