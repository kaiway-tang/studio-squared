using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sparkle : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] Vector3 scale;
    int life;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        life++;
        
        if (life < 8)
        {
            trfm.localScale += scale;
        } else
        {
            trfm.localScale -= scale;
            
            if (life > 13)
            {
                Destroy(gameObject);
            }
        }

        trfm.Rotate(Vector3.forward * 10);
    }
}
