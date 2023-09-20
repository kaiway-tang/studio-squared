using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageMinecartController : MonoBehaviour
{
    [SerializeField] Rigidbody2D minecartRb;
    [SerializeField] Collider2D col;
    [SerializeField] ParentingTrigger parenter;
    [SerializeField] SceneEdge sceneEdge;
    bool transitionPrepared;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (parenter.playerIsChild)
        {
            if (minecartRb.velocity.x > -22)
            {
                minecartRb.velocity += Vector2.right * -.3f;
            }
            if (!transitionPrepared)
            {
                Player.self.Stun(999);
                col.enabled = false;
                sceneEdge.nextScene = "MinecartScene";
                transitionPrepared = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
