using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : HPEntity
{
    [SerializeField] float speed, swingWidth, widthDampening, speedDampening;
    float defaultSwingWidth, defaultSpeed;

    [SerializeField] int widthFactor, speedFactor;
    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
        speed *= Random.Range(.9f, 1.1f);
        period = Random.Range(0f, 6.28f);

        defaultSpeed = speed;
        defaultSwingWidth = swingWidth;
    }

    Vector3 rotation;
    [SerializeField] float period;
    new void FixedUpdate()
    {
        base.FixedUpdate();
        period += 3.14f * speed;
        if (period > 6.28) { period -= 6.28f; }
        rotation.z = Mathf.Sin(period) * swingWidth;
        trfm.localEulerAngles = rotation;

        if (swingWidth > defaultSwingWidth)
        {
            swingWidth *= widthDampening;
            if (swingWidth < defaultSwingWidth)
            {
                swingWidth = defaultSwingWidth;
            }
        }
        if (speed > defaultSpeed)
        {
            speed *= speedDampening;
            if (speed < defaultSpeed)
            {
                speed = defaultSpeed;
            }
        }
    }

    protected override void OnDamageTaken(int amount, int result)
    {
        if (GameManager.playerTrfm.position.x > trfm.position.x)
        {
            period = 1.5f;
        }
        else
        {
            period = .5f;
        }

        swingWidth = defaultSwingWidth * widthFactor;
        speed = defaultSpeed * speedFactor;
    }
}
