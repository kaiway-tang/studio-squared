using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    [SerializeField] bool functional;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] int timer;
    [SerializeField] ParentingTrigger parenter;
    [SerializeField] SimpleAnimator animator, wheels;
    static bool played;
    // Start is called before the first frame update
    void Start()
    {      
        if (played)
        {
            timer = -999;
            functional = false;
            Trash();
        }
        else
        {
            if (functional)
            {
                Player.self.SetFrozen(true);
                Player.self.SetFacing(MobileEntity.LEFT);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer--;

        if (functional)
        {
            if (timer == 28)
            {
                CameraController.SetMode(CameraController.FALLING);
            }

            if (timer > 0)
            {
                rb.velocity = Vector2.right * speed + Vector2.up * rb.velocity.y;
                Player.self.SetXVelocity(speed);
                GameManager.playerTrfm.position = new Vector3(transform.position.x, GameManager.playerTrfm.position.y, GameManager.playerTrfm.position.z);
            }

            if (timer < 1)
            {
                Destroy(parenter);
                Player.self.SetFacing(MobileEntity.RIGHT);
                CameraController.AddTrauma(25);
                functional = false;
                rb.velocity += Vector2.down * 15;
                Invoke("Trash", 2);
            }
        }

        if (timer < 1 && timer > -200)
        {
            GameManager.playerTrfm.position = new Vector3(12.1f, GameManager.playerTrfm.position.y, 0);
            Player.self.SetXVelocity(0);
            GameManager.playerTrfm.parent = null;
        }

        if (timer == -260) { Player.self.SetFrozen(false); played = true;}
    }

    void Trash()
    {
        Destroy(GetComponent<Rigidbody2D>());
        animator.Stop(true);
        wheels.Stop(true);
        transform.position = new Vector3(16.5f, -13.2f, 0);
        transform.eulerAngles = new Vector3(0,0,120);
    }
}
