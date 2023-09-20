using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float swingWidth;
    Transform trfm;
    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
        speed *= Random.Range(.9f, 1.1f);
        period = Random.Range(0f, 6.28f);
    }

    Vector3 rotation;
    float period;
    void FixedUpdate()
    {
        period += 3.14f * speed;
        if (period > 6.28) { period = 0; }
        rotation.z = Mathf.Sin(period) * swingWidth;
        trfm.localEulerAngles = rotation;
    }
}
