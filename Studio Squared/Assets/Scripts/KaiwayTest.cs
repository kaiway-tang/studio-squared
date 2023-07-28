using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiwayTest : MonoBehaviour
{
    [SerializeField] HPEntity hpEntity;
    [SerializeField] Transform scalerTrfm;

    private void FixedUpdate()
    {
        scalerTrfm.localScale = new Vector3((float)hpEntity.HP/hpEntity.maxHP, 1, 1);
    }
}