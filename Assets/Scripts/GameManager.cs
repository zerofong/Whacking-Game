using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public ShapeController shapeController;

    public GameObject mainMenuGO;
    public GameObject gameplayGO;
    public GameObject gameOverGO;

    [Header("Game Setting")]
    public TextMeshProUGUI timerText;
    public float gameDuration = 60f;

    private float timer = 0;
    private bool isGameStart;
    private GameStateEnums gameState;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        InitGame();
    }

    void Update()
    {
        if (!isGameStart) return;

        UpdateTimer();
    }

    #region Timer
    private void UpdateTimer()
    {
        timer -= Time.deltaTime;
        timerText.text = ((int)timer).ToString();

        if (timer <= 0)
        {
            timer = gameDuration;
            GameOver();
        }
    }
    #endregion

    #region Game Controls
    [ContextMenu("Init")]
    public void InitGame()
    {
        timer = gameDuration;
        ChangeGameStateScene(GameStateEnums.MainMenu);
        shapeController.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        shapeController.ResetShape();
        ScoreManager.OnScoreResetAction?.Invoke();
        ChangeGameStateScene(GameStateEnums.Gameplay);
        isGameStart = true;
        shapeController.enabled = true;
    }

    public void BackToMenu()
    {
        ChangeGameStateScene(GameStateEnums.MainMenu);
        shapeController.ResetShape();
    }

    public void GameOver()
    {
        isGameStart = false;
        shapeController.enabled = false;
        shapeController.ResetShape();
        ChangeGameStateScene(GameStateEnums.GameOver);
    }

    public bool GetGameStatus() => isGameStart;

    public void ChangeGameStateScene(GameStateEnums state)
    {
        switch (state)
        {
            case GameStateEnums.MainMenu:
                mainMenuGO.SetActive(true);
                gameplayGO.SetActive(false);
                gameOverGO.SetActive(false);
                break;
            case GameStateEnums.Gameplay:
                mainMenuGO.SetActive(false);
                gameplayGO.SetActive(true);
                gameOverGO.SetActive(false);
                break;
            case GameStateEnums.GameOver:
                mainMenuGO.SetActive(false);
                gameplayGO.SetActive(false);
                gameOverGO.SetActive(true);
                break;
            default:
                break;
        }
    }
    #endregion
}

public enum GameStateEnums { MainMenu, Gameplay, GameOver}
