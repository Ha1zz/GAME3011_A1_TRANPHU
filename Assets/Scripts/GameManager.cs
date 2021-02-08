using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
public enum GameState
{
    SCAN = 0,
    COLLECT
}
public class GameManager : MonoBehaviour
{
    public ResourceManager resourceManager;
    public int playerScore = 0;
    public int playerScanLimit = 6;
    public int playerCollectLimit = 3;
    public TextMeshProUGUI scanLimit;
    public TextMeshProUGUI collectLimit;

    public GameState currentState = 0;
    public TextMeshProUGUI resourceTotal;

    public static GameManager instance;

    public TextMeshProUGUI totalScore;
    public GameObject winPanel;
    public GameObject canvas;
    public TextMeshProUGUI gameStatus;

    private void Awake()
    {
        winPanel.SetActive(false);
        scanLimit.text = playerScanLimit.ToString();
        collectLimit.text = playerCollectLimit.ToString();
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        gameStatus.text = "SCANMODE";
        canvas.SetActive(false);
    }

    public event Action<int, int> ScanMineralEvent;
    public event Action<int, int> CollectMineralEvent;
    public void OnScanMineral(int x,int y)
    {
        ScanMineralEvent?.Invoke(x, y);
    }

    public void OnCollectMineral(int x,int y)
    {
        CollectMineralEvent?.Invoke(x, y);
    }

    public void UpdateLimit()
    {
        if (currentState == GameState.SCAN)
        {
            playerScanLimit--;
            scanLimit.text = playerScanLimit.ToString();
        }
        if (currentState == GameState.COLLECT)
        {
            playerCollectLimit--;
            collectLimit.text = playerCollectLimit.ToString();
        }
        if (playerCollectLimit <= 0)
        {
            FinishTheGame();
        }
    }
    public void CollectMode()
    {
        currentState = GameState.COLLECT;
        gameStatus.text = "COLLECTMODE";
    }

    public void ScanMode()
    {
        currentState = GameState.SCAN;
        gameStatus.text = "SCANMODE";
    }

    public void SetScore(int value)
    {
        playerScore += value;
        resourceTotal.text = playerScore.ToString();
    }

    public void ShowTheMinigame()
    {
        canvas.SetActive(true);
        resourceManager.SpawnEverything();
    }

    public void FinishTheGame()
    {
        totalScore.text = playerScore.ToString();
        winPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
