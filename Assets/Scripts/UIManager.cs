using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _pauseTint;
    [SerializeField] GameObject _gameOverOverlay;
    [SerializeField] TextMeshProUGUI _gameOverScore;

    ScoreSystem _scoreSystem;

    // Start is called before the first frame update
    void Awake()
    {
        _scoreSystem = GetComponent<ScoreSystem>();

        _pauseTint.SetActive(false);
        _gameOverOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0;

            _pauseTint.SetActive(true);
        }
        else if (Time.timeScale != 1.0f)
        {
            Time.timeScale = 1.0f;

            _pauseTint.SetActive(false);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _gameOverScore.text = _scoreSystem.Score.ToString();
        _gameOverOverlay.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1.0f;
    }
}
