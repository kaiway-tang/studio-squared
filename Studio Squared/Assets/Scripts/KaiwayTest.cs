using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiwayTest : MonoBehaviour
{
    [SerializeField] HPEntity hpEntity;
    // Start is called before the first frame update
    void Start()
    {
        hpEntity.TakeDamage(20);
        GetComponent<Rigidbody2D>().velocity = new Vector2(2,0);
    }
}