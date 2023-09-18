using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    Collider2D coin;

    // Start is called before the first frame update
    void Start()
    {
        coin = GetComponent<Collider2D>();
    }

    public void CheckValue()
    {
        /*
        if(coin != wrong)
        {
            
        }
        else if(coin == wrong)
        {
            
        }
        else
        {

        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
