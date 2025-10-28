using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _gameOverCanvas;

    public bool IsStarted { get; private set; } = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        Time.timeScale = 0f; 
        IsStarted = false;

        if (_gameOverCanvas != null)
            _gameOverCanvas.SetActive(false);
    }

    public void StartGame()
    {
        if (IsStarted) return;

        IsStarted = true;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        if (_gameOverCanvas != null)
            _gameOverCanvas.SetActive(true);

        Time.timeScale = 0f; 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
