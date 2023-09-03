using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MobileEntity
{
    [SerializeField] float leapDuration;
    [SerializeField] EnemyDemoAttack attack;
    int attackCooldown, attackTimer;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        attackCooldown = Random.Range(70, 130);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) { Leap(); }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attackCooldown > 0)
        {
            attackCooldown--;
        }
        else
        {
            Leap();
            attackCooldown = Random.Range(70, 130);
        }

        if (attackTimer > 0)
        {
            if (attackTimer == 1)
            {
                attack.Activate(-1, 4);
            }
            attackTimer--;
        }
        else
        {
            ApplyXFriction(.3f);
        }
    }

    void Leap()
    { 
        attackTimer = 35;
        SetXVelocity((Player.GetPredictedPosition(leapDuration, true).x - trfm.position.x)/leapDuration);
        SetYVelocity(leapDuration * rb.gravityScale * 5);
    }
}
