using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    bool isEnabled;

    public GameObject[] Coins;
    public int CoinLimit;
    public Vector2 MinimumPos;
    public Vector2 MaximumPos;

    void Awake()
    {
        isEnabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown("enter"))
        {
            SpawnCoin();
        }

        if (Input.GetKeyDown("space") && isEnabled != true)
        {
            EnableSpawner();
        }
        else if(Input.GetKeyDown("space") && isEnabled == true)
        {
            DisableSpawner();
        }
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

    public void EnableSpawner()
    {
        isEnabled = true;
        int i = 0;

        Debug.Log("Coin Spawner enabled.");

        while(i < CoinLimit)
        {
            SpawnCoin();
            i++;
        }

        //DisableSpawner();
    }

    public void DisableSpawner()
    {
        isEnabled = false;

        Debug.Log("Coin Spawner disabled.");
    }
}
