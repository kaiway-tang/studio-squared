using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class TextManager : MonoBehaviour
{

    public static Dictionary<string, int> VarStorage;
    public static TextManager Instance;


    public DialogueRunner dialogueRunner;

    public bool monitorDialog;



    UnityEvent dialogStartEvent;
    UnityEvent dialogEndEvent;


    public Player player;
    public PlayerDialogInteract playerDialogInteract;

    private NPC currentNpc;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            VarStorage = new Dictionary<string, int>();
            monitorDialog = false;

            dialogStartEvent = dialogueRunner.onDialogueStart;
            dialogEndEvent = dialogueRunner.onDialogueComplete;
            dialogStartEvent.AddListener(DialogStart);
            dialogEndEvent.AddListener(DialogEnd);
            //varStore = GetComponent<InMemoryVariableStorage>();

        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        var temp = GameObject.Find("Player"); //does this get called at the loading of a new scene?
        player = temp.GetComponentInChildren<Player>();
        playerDialogInteract = temp.GetComponentInChildren<PlayerDialogInteract>();
    }


    [YarnCommand("SetVar")]
    public void SetVar(string name, int number)
    {
        Debug.Log("Setting dict for:" + name);
        if (VarStorage.ContainsKey(name))
        {
            VarStorage[name] = number;
        }
        else
        {
            VarStorage.Add(name, number);
        }
    }

    public class GetVarFunc
    {
        [YarnFunction("GetVar")]
        public static int GetVar(string name)
        {
            Debug.Log("Checking dict for:" + name);

            if (VarStorage.ContainsKey(name))
            {
                Debug.Log(VarStorage[name]);
                return VarStorage[name];
            }
            else
            {
                Debug.Log(0);
                return 0;
            }
        }
    }



    //yarn commands
    /*
    [YarnCommand("SetVar")]
    public void SetStageYarn(string NPC, int stage)
    {
        SetNPCTalkStage(NPC, stage);
    }
    */


    [YarnCommand("ChangeScene")]
    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    [YarnCommand("ResetVariables")]
    public void resetVariables()
    {
        VarStorage = new Dictionary<string, int>();
    }


    public void RunNPCDialog(NPC _npc)
    {
        dialogueRunner.StartDialogue(_npc.dialog);
    }




    [SerializeField] public GameObject demoBox; //TODO: REMOVE

    [YarnCommand("DemoDrop")]
    public void DemoDrop()
    {
        for (int i = 0; i < 10; i++)
        { 
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y + Random.Range(-2, 2), transform.position.z + Random.Range(-2, 2));
            GameObject.Instantiate(demoBox, pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }



    public void DialogStart()
    {
        currentNpc = playerDialogInteract.npc;
        playerDialogInteract.npc = null;
        player.SetFrozen(true);
    }

    public void DialogEnd()
    {
        player.SetFrozen(false);
        playerDialogInteract.npc = currentNpc;
    }






}