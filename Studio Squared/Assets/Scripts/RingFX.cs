using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFX : PooledObject
{
    [SerializeField] Vector3 startScale = new Vector3(.4f, .4f, 1);
    [SerializeField] float scaleRate;
    [SerializeField] Fader fader;
    [SerializeField] bool fade;
    // Start is called before the first frame update
    new void OnEnable()
    {
        base.OnEnable();
        trfm.localScale = startScale;
        fader.SetAlpha(1);
        if (fade) { fader.FadeOut(1f / life); }
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        trfm.localScale *= scaleRate;
    }
}
