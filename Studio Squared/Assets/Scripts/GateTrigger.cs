using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] bool triggered;
    [SerializeField] Gate gate;
    void Start()
    {
        Destroy(GetComponent<SpriteRenderer>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!triggered)
        {
            gate.Close();
            triggered = true;
        }
    }
}
