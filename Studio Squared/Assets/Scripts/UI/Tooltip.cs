using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDesc;
    [SerializeField] TextMeshProUGUI itemPrice;
    

    public void SetData(string itemName, string itemDesc, int itemPrice)
    {
        this.itemName.text = itemName;
        this.itemDesc.text = itemDesc;
        this.itemPrice.text = itemPrice.ToString();
    }
}
