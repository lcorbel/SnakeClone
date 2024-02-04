using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    private int _currentScore;
    private int _highScore;
    public TMP_Text _scoreText;
    public TMP_Text _highScoreText;
    public GameObject _gameOverScreen;
    public GameObject _welcomeScreen;
    public Boolean _isWelcomeScreen;

    private void Start()
    {
        _currentScore = -1;
        IncrementScore();
        _highScore = PlayerPrefs.GetInt("hs");
        _highScoreText.SetText("HighScore : " + _highScore);
    }

    public void IncrementScore()
    {
        _currentScore++;
        if(_currentScore > _highScore)
        {
            PlayerPrefs.SetInt("hs", _currentScore);
            _highScoreText.SetText("HighScore : " + _currentScore);
        }
        _scoreText.SetText("Score : " +_currentScore);
    }

    public void GameOver()
    {
        _gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        _welcomeScreen.SetActive(false);
        _isWelcomeScreen = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
