using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiwayTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(2,0);
    }
}