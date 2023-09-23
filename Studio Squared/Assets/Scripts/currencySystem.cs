using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CurrencySystem : MonoBehaviour
{
    [SerializeField] private Money ones;
    [SerializeField] private Money fives;
    [SerializeField] private Money tens;
    [SerializeField] private Money fifties;
    [SerializeField] private int amountSpawn;

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     SpawnCoins(amountSpawn);
        // }
    }

    public void SpawnCoins(int amount)
    {
        int numOnes = amount < 10 ? amount : 10;
        for (int i = 0; i < numOnes; i++)
        {
            float offset = Random.Range(-0.1f, 0.5f);

            Instantiate(ones, new Vector2(transform.position.x + offset, transform.position.y), quaternion.identity);
        }

        amount -= 10;
        while (amount > 0)
        {
            float offset = Random.Range(-0.5f, 0.5f);
            Debug.Log(amount);
            Debug.Log(offset);
            if (amount < 5)
            {
                Instantiate(ones, new Vector2(transform.position.x + offset, transform.position.y), quaternion.identity);
                amount--;
            }

            else if (amount < 10)
            {
                Instantiate(fives,new Vector2(transform.position.x + offset, transform.position.y), quaternion.identity);
                amount -= 5;
            }
            else if (amount <20)
            {
                Instantiate(tens,new Vector2(transform.position.x + offset, transform.position.y), quaternion.identity);
                amount -= 10;
            }
            else if (amount > 50)
            {
                Instantiate(fifties,new Vector2(transform.position.x + offset, transform.position.y), quaternion.identity);
                amount -= 50;
            }
        }
    }
}