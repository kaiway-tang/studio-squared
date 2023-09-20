using UnityEditor;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    [SerializeField] private Money ones;
    [SerializeField] private Money fives;
    [SerializeField] private Money tens;
    [SerializeField] private Money fifties;
    public void SpawnCoins(int amount)
    {
        int numOnes = amount < 10 ? amount : 10;
        for (int i = 0; i < numOnes; i++)
        {
            Instantiate(ones);
        }

        amount -= 10;
        while (amount > 0)
        {
            if (amount < 5)
            {
                Instantiate(ones);
                amount--;
            }

            else if (amount < 10)
            {
                Instantiate(fives);
                amount -= 5;
            }
            else if (amount <20)
            {
                Instantiate(tens);
                amount -= 10;
            }
            else if (amount > 50)
            {
                Instantiate(fifties);
                amount -= 50;
            }
        }
    }
}