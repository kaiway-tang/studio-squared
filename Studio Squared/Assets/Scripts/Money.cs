using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private int value;
    [SerializeField] private TMP_Text playerCoins;
    
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.CompareTag("Player"))
        {

            playerCoins.text = Player.self.updateCoins(value).ToString();
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
