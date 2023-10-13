using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] MenuManager menuManager;
    [SerializeField] string itemName;
    [SerializeField] string itemDesc;
    [SerializeField] int itemPrice;

    public void OnSelect(BaseEventData eventData)
    {
        menuManager.toolTip.SetData(itemName, itemDesc, itemPrice);
    }
}
