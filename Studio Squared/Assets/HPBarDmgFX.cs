using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarDmgFX : Fader
{
    protected override void FadeInComplete()
    {
        FadeOut(0.02f, 0);
    }
}
