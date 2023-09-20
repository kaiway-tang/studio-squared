using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentingTrigger : MonoBehaviour
{
    [SerializeField] Transform parentTrfm;
    public bool playerIsChild;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == GameManager.PlayerTriggerLayer && col.gameObject.tag == "Player")
        {
            playerIsChild = true;
            GameManager.playerTrfm.parent = parentTrfm;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == GameManager.PlayerTriggerLayer && col.gameObject.tag == "Player")
        {
            playerIsChild = false;
            GameManager.playerTrfm.parent = null;
        }
    }
}
