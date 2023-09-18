using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentingTrigger : MonoBehaviour
{
    [SerializeField] Transform parentTrfm;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == GameManager.PlayerTriggerLayer && col.gameObject.tag == "Player")
        {
            GameManager.playerTrfm.parent = parentTrfm;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == GameManager.PlayerTriggerLayer && col.gameObject.tag == "Player")
        {
            GameManager.playerTrfm.parent = null;
        }
    }
}
