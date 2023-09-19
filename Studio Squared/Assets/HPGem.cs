using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPGem : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] float bobRate, bobHeight, rotateRate;
    // Start is called before the first frame update
    void Start()
    {
        bob = trfm.position;
    }

    Vector3 bob;
    float period;
    void FixedUpdate()
    {
        trfm.Rotate(Vector3.forward * rotateRate);
        period += bobRate;
        bob.y = .866f + Mathf.Sin(period) * bobHeight;
        trfm.localPosition = bob;
    }

    void Disappear()
    {
        Destroy(gameObject);
    }

    bool used;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!used && col.gameObject.layer == GameManager.PlayerTriggerLayer)
        {
            used = true;
            Player.self.Heal(12);
            Disappear();
        }
    }
}
