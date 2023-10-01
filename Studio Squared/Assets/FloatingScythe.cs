using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScythe : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] SimpleAnimator dematerialize, materialize;
    [SerializeField] GameObject light2D;
    [SerializeField] float close, far;
    [SerializeField] TrailRenderer trail;
    Transform trfm;
    int lightTimer, materializeTimer;
    public static bool trailEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
        materialized = isActiveAndEnabled;
        trail.emitting = GetComponent<SpriteRenderer>().sprite != null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (materializeTimer > 0)
        {
            materializeTimer--;
            if (materializeTimer < 1)
            {
                Materialize();
            }
        }
        if (lightTimer > 0)
        {
            lightTimer--;
            if (lightTimer == 0)
            {
                light2D.SetActive(false);
            }
        }
        HandleTracking();
    }

    Vector3 vect3;
    bool facingLeft;
    void HandleTracking()
    {
        if (Vector2.Distance(targetPos.position, trfm.position) < close)
        {
            if (trail.emitting) { trail.emitting = false; }
            trfm.up += (Vector3.up - trfm.up) * .15f;
        }
        else
        {
            if (!trail.emitting && materialized && trailEnabled) { trail.emitting = true; }
            trfm.up += ((targetPos.position - trfm.position) - trfm.up) * .1f;
        }
        trfm.position += (targetPos.position - trfm.position) * .1f;

        if (facingLeft)
        {
            if (targetPos.position.x > trfm.position.x)
            {
                FlipFacing();
                facingLeft = false;
            }
        }
        else
        {
            if (targetPos.position.x < trfm.position.x)
            {
                FlipFacing();
                facingLeft = true;
            }
        }
    }

    void FlipFacing()
    {
        vect3 = trfm.localScale;
        vect3.x *= -1;
        trfm.localScale = vect3;
    }

    bool materialized;
    public void Dematerialize(int duration = 0)
    {
        if (!materialized) return;
        trail.emitting = false;
        lightTimer = 4;
        light2D.SetActive(true);
        dematerialize.Play();
        materialized = false;
        materializeTimer = duration;
    }

    public void Materialize()
    {
        if (materialized) return;
        lightTimer = 4;
        light2D.SetActive(true);
        trfm.position = targetPos.position;
        materialize.Play();
        materialized = true;
    }
}
