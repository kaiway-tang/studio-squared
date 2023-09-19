using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    Transform trfm;
    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        trfm.Rotate(Vector3.forward * rotationSpeed);
    }
}
