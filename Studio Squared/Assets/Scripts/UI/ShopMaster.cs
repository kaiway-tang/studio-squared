using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMaster : MonoBehaviour
{
    public void ItemSwitch(string item)
    {
        switch (item)
        {
            case "Test":
                TestCall();
                break;
            default:
                Debug.Log("Issue with shop - went to default case.");
                break;
        }
    }

    private void TestCall()
    {
        Debug.Log("Hello World!");
    }
}
