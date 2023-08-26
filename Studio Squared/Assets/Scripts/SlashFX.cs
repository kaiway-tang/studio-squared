using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashFX : PooledObject
{
    [SerializeField] Vector3 initialScale, changeScale;
    static Vector3 vect3;
    // Start is called before the first frame update

    private new void OnEnable()
    {
        base.OnEnable();
        trfm.localScale = initialScale;
        vect3.z = 1;
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        vect3.x = trfm.localScale.x * changeScale.x;
        vect3.y = trfm.localScale.y + changeScale.y;
        trfm.localScale = vect3;
    }
}
