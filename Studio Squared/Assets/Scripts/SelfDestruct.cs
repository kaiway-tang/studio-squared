using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float life; //in seconds
    
    void Start()
    {
        Destroy(gameObject, life);
    }
}
