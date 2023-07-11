using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    [SerializeField] protected int maxHP, HP;
    [SerializeField] protected Transform trfm;
    [SerializeField] EntityType entityType;

    public enum EntityType
    {
        Enemy, Player, Neutral
    }

    protected void Start()
    {
        if (HP == 0) { HP = maxHP; }
    }

    public void TakeDamage(int amount, EntityType entitySource)
    {
        if (entitySource == entityType) { return; }

        HP -= amount;

        if (HP < 0)
        {
            //Die
        }
    }
}
