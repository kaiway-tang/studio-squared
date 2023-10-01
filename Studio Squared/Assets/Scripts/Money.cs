using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Money : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private int value;
    [SerializeField] private TextMeshProUGUI playerCoins;
    private Vector2 off;
    [SerializeField] private GameObject parent;
    [SerializeField]private int moveDuration = 1;
    private float delay = 0;
    void Awake()
    {
        off = new Vector2(Random.Range(-3f, 3f), Random.Range(0f, 3f));
        parent.GetComponent<Rigidbody2D>().velocity = off;
        playerCoins = GameObject.Find("num_coins").GetComponent<TextMeshProUGUI>();
    }
    
    private void FixedUpdate()
    {
        transform.position = parent.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("destroy");
            Destroy(parent);
            playerCoins.text = Player.self.updateCoins(value).ToString();
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
