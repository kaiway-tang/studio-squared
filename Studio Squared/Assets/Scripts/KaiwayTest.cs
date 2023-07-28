using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiwayTest : MonoBehaviour
{
    int airtime;

    private void FixedUpdate()
    {
        airtime++;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("hit: " + col.gameObject + "after: " + airtime + " ticks");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        airtime = 0;
    }
}