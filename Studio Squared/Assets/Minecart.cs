using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    [SerializeField] bool functional;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] int timer;
    // Start is called before the first frame update
    void Start()
    {
        if (functional)
        {
            Player.self.SetFrozen(true);
            Player.self.SetFacing(MobileEntity.LEFT);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (functional)
        {
            timer--;

            if (timer > 11)
            {
                rb.velocity = Vector2.right * speed + Vector2.up * rb.velocity.y;
                Player.self.SetXVelocity(speed);
            }

            if (timer < 1)
            {
                functional = false;
                Player.self.SetFrozen(false);
                GameManager.playerTrfm.parent = null;
                Invoke("Trash", 2);
            }
        }
    }

    void Trash()
    {
        Destroy(GetComponent<Rigidbody2D>());
        transform.position = new Vector3(7, -13.3f, 0);
        transform.eulerAngles = new Vector3(0,0,120);
    }
}
