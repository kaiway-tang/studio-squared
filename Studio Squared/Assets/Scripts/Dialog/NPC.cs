using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;

public class NPC : MonoBehaviour
{

    [SerializeField] public string dialog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if(collision.tag == "Player")
        {
            collision.GetComponentInParent<PlayerDialogInteract>().SetNPC(this);
        }
    }

    /*
    public void StartDialog()
    {
        Debug.Log("Hello World!");
    }*/
}
