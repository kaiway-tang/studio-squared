using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingBar : MonoBehaviour
{
    [SerializeField] float maxX;
    [SerializeField] Transform scalerTrfm;

    static Vector3 vect3 = Vector3.one;
    public void SetPercentage(float percentage)
    {
        vect3.x = percentage * maxX;
        scalerTrfm.localScale = vect3;
    }
}