using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Money : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int value;
    [SerializeField] float speed, turnSpd;
    [SerializeField] private TextMeshProUGUI playerCoins;

    [SerializeField] Transform emptyTrfm;
    private float delay = 0;
    Vector3 velocity;
    void Awake()
    {
        emptyTrfm.Rotate(Vector3.forward * Random.Range(0, 360));
        playerCoins = GameObject.Find("num_coins").GetComponent<TextMeshProUGUI>();
    }
    
    private void FixedUpdate()
    {
        transform.position += emptyTrfm.up * speed;
        emptyTrfm.eulerAngles += (GameManager.playerTrfm.eulerAngles - emptyTrfm.eulerAngles) * turnSpd;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.layer == GameManager.PlayerTriggerLayer)
        {
            playerCoins.text = Player.self.updateCoins(value).ToString();
            Destroy(gameObject);
        }        
    }
}
