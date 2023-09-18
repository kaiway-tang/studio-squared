using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTrigger : MonoBehaviour
{
    public int isTouching;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        isTouching++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching--;
    }
}
