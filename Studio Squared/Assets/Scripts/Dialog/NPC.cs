using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPC : MonoBehaviour
{

    [SerializeField] public string dialog;
    [SerializeField] GameObject shop; //optional
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


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponentInParent<PlayerDialogInteract>().RemoveNPC(this);
        }
    }
    /*
    public void StartDialog()
    {
        Debug.Log("Hello World!");
    }*/

    [YarnCommand("OpenShop")]
    public void OpenShop()
    {
        //call a gamemanager function to freeze the player here
        shop.SetActive(true);
    }



}


