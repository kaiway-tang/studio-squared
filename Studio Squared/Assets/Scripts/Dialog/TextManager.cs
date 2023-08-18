using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{

    public static Dictionary<string, int> VarStorage;
    public static TextManager Instance;

    public DialogueRunner dialogueRunner;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            VarStorage = new Dictionary<string, int>();
            //varStore = GetComponent<InMemoryVariableStorage>();

        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

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
}