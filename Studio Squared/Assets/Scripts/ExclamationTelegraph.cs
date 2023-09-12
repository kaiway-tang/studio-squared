using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationTelegraph : PooledObject
{
    [SerializeField] Vector3 rise;
    new void OnEnable()
    {
        base.OnEnable();
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();
        trfm.position += rise;
    }
}
