using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject[] Coins;
    public float SpawnInterval;
    public float MinimumInterval;
    public int IntervalModifier;
    public Vector2 MinimumPos;
    public Vector2 MaximumPos;

    void Awake()
    {
        StartCoroutine(SpawnCoins(SpawnInterval));
    }

    IEnumerator SpawnCoins(float interval)
    {
        yield return new WaitForSeconds(interval);
        
        SpawnCoin();
        // DecreaseInterval();

        StartCoroutine(SpawnCoins(SpawnInterval));
    }

    void InstantiateCoin(int coinValue, Vector2 position)
    {
        Instantiate(Coins[coinValue], position, Quaternion.identity);
    }

    Vector2 GetRandomPosition()
    {
        Vector2 randomPosition = new Vector2(
            Random.Range(MinimumPos.x, MaximumPos.x),
            Random.Range(MinimumPos.y, MaximumPos.y)
            );

        return randomPosition;
    }
    public void SpawnCoin()
    {
        Vector2 position = GetRandomPosition();
        InstantiateCoin(Random.Range(0, Coins.Length), position);
    }

    public void DecreaseInterval()
    {
        float newSpawnInterval;

        newSpawnInterval = SpawnInterval - SpawnInterval / IntervalModifier;

        if (newSpawnInterval < MinimumInterval)
        {
            SpawnInterval = MinimumInterval;
        }
        else
        {
            SpawnInterval = newSpawnInterval;
        }
    }
}
