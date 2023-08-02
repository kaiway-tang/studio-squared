using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntity : StateEntity
{

    [SerializeField] public BossEnv bossEnv;

    public Dictionary<string, List<State>> randStateDict;

    public string currentStage;


    public void AddRandState(string stateListName, State state)
    {
        try
        {
            randStateDict[stateListName].Add(state);
        }
        catch
        {
            randStateDict[stateListName] = new List<State>() { state };

        }
    }

    public State SelectRandState()
    {
        return randStateDict[currentStage][Random.Range(0, randStateDict[currentStage].Count)];
    }

    public void ChangeStage(string stage)
    {
        currentStage = stage;
    }
}
