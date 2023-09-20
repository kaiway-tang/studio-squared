using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectDodge : HPEntity
{
    [SerializeField] int window;
    [SerializeField] GameObject ringFXObj;
    [SerializeField] int timer;
    bool ringSpawned;

    [SerializeField] PooledObject pooledObject;

    private void OnEnable()
    {
        timer = window;
        ringSpawned = false;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        if (timer > 0)
        {
            timer--;
            if (timer < 1)
            {
                pooledObject.Destantiate();
            }
        }
    }

    protected override void OnDamageTaken(int amount, int result)
    {
        Player.self.Heal((int)(amount * .5f));

        if (!ringSpawned)
        {
            Player.self.perfectDodgeRingPooler.Instantiate(trfm.position, 0);
            ringSpawned = true;
        }
    }
}
