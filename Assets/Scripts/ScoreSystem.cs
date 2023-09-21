using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreInterface;
    [SerializeField] TextMeshProUGUI _healthInterface;

    UIManager _UIManager;

    public int Score;
    public int Health;
    public int MaxHealth;
    
    void Awake()
    {
        _UIManager = GetComponent<UIManager>();

        _scoreInterface.text = Score.ToString();
        _healthInterface.text = Health.ToString();
    }

    void Update()
    {
        
    }

    public void IncreaseScore()
    {
        Score++;
        _scoreInterface.text = Score.ToString();
        Debug.Log("Score increased to: " + Score);

        IncreaseHealth();
    }

    /*
    // Probably not going to need this
    public void DecreaseScore()
    {
        
    }*/

    public void IncreaseHealth()
    {
        if (Health < MaxHealth)
        {
            Health++;
            _healthInterface.text = Health.ToString();
            Debug.Log("Health increased to: " + Health);
        }
    }

    public void DecreaseHealth(int subtrahend)
    {
        Health -= subtrahend;

        if (Health <= 0)
        {
            Health = 0;

            _UIManager.GameOver();
        }

        _healthInterface.text = Health.ToString();
        Debug.Log("Health decreased to: " + Health);
    }
}
