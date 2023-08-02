using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateDict
{
    public Dictionary<string, List<State>> dict;

    public StateDict()
    {
        dict = new Dictionary<string, List<State>>();
    }

    public void AddStateList(List<State> list, string stateListName = "1")
    {
        if (dict.ContainsKey(stateListName))
        {
            dict[stateListName].AddRange(list);
        }
        else
        {
            dict[stateListName] = list;
        }
    }
    public void AddRandState(State state, string stateListName = "1")
    {
        try
        {
            dict[stateListName].Add(state);
        }
        catch
        {
            dict[stateListName] = new List<State>() { state };

        }
    }

    public State SelectRandState(string stateListName = "1") //stages start at 1
    {
        return dict[stateListName][Random.Range(0, dict[stateListName].Count)];
    }
}



public class BossEntity : StateEntity
{

    //[SerializeField] public BossEnv bossEnv;



    public string currentStage;

    public StateDict stateDict;

    public void ChangeStage(string stage)
    {
        currentStage = stage;
    }
}
