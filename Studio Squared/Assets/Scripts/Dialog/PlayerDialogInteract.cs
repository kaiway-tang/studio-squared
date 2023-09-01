using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDialogInteract : MonoBehaviour
{

    public NPC npc;
    public TextManager textManager;

    // Start is called before the first frame update
    void Start()
    {
        npc = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            InteractWithNPC();
        }
    }

    public void SetNPC(NPC _npc)
    {
        npc = _npc;
    }

    public void RemoveNPC(NPC _npc)
    {
        if(npc == _npc)
        {
            npc = null;
        }
    }

    public void InteractWithNPC()
    {
        if (npc)
        {
            textManager.RunNPCDialog(npc);//npc.StartDialog();
           // npc = null; //moved to textmanager
            Debug.Log("Interacting!!!");
        }
        else
        {
            //Debug.Log("Nobody to interact with!");
        }

    }
}
