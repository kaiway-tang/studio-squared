using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawlet : MobileEntity
{
    [SerializeField] float spd, turnRate;
    [SerializeField] EnemyHelpers helper;
    [SerializeField] bool allTerrain;
    int hurtTimer, lockFloorTrigger;
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (stunned > 0) return;

        if (TerrainTriggerTouching(1)) //head trigger touching
        {
            if (allTerrain)
            {
                if (IsFacingRight())
                {
                    trfm.Rotate(Vector3.forward * turnRate);
                    trfm.position += trfm.right * spd * .5f;
                }
                else
                {
                    trfm.Rotate(Vector3.forward * -turnRate);
                    trfm.position += trfm.right * -spd * .5f;
                }

                lockFloorTrigger = 10;
            }
            else
            {
                Stun(5);
                FlipFacing();
            }
        } else if (!TerrainTriggerTouching(0) && lockFloorTrigger < 1) //floor trigger touching
        {
            if (IsFacingRight())
            {
                trfm.Rotate(Vector3.forward * -turnRate);
                trfm.position += trfm.right * spd * .5f;
            }
            else
            {
                trfm.Rotate(Vector3.forward * turnRate);
                trfm.position += trfm.right * -spd * .5f;
            }
        }
        else
        {
            if (IsFacingRight())
            {
                trfm.position += trfm.right * spd;
            }
            else
            {
                trfm.position += trfm.right * -spd;
            }
        }

        if (lockFloorTrigger > 0)
        {
            lockFloorTrigger--;
        }
    }

    protected override void OnDamageTaken(int amount, int result)
    {
        if (Mathf.Abs(trfm.localEulerAngles.z) > 170) { SetFacing(GameManager.playerTrfm.position.x < trfm.position.x); }
        else { SetFacing(GameManager.playerTrfm.position.x > trfm.position.x); }
        helper.FlashWhite();
        Stun(5);
    }
}
