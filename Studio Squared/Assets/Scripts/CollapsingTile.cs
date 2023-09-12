using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingTile : MonoBehaviour
{
    [SerializeField] CollapsingTile[] neighborTiles;
    [SerializeField] Collider2D col;
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool collapsed;
    public void Collapse()
    {
        if (!collapsed)
        {
            collapsed = true;

            //col.enabled = false;
            rb.isKinematic = false;
            rb.gravityScale = 9;
            Destroy(gameObject, 3);
            Invoke("CollapseNeighbors", .07f);
        }
    }

    void CollapseNeighbors()
    {
        neighborTiles[0].Collapse();
        neighborTiles[1].Collapse();
    }
}
