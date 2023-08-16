using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectDodge : HPEntity
{
    [SerializeField] int window;
    [SerializeField] GameObject ringFXObj;
    int timer;
    bool ringSpawned;

    [SerializeField] PooledObject pooledObject;

    private void OnEnable()
    {
        timer = window;
        ringSpawned = false;
    }

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            timer--;
            if (timer < 1)
            {
                pooledObject.Destantiate();
            }
        }
    }

    protected override void OnDamageTaken(int amount)
    {
        Player.self.Heal((int)(amount * .5f));

        if (!ringSpawned)
        {
            Instantiate(ringFXObj, trfm.position, trfm.rotation);
            ringSpawned = true;
        }
    }
}
