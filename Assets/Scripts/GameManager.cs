﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public GameObject gameOverScreen;

    public GameObject victoryScreen;

    public int AvailibleLives = 1;
    public int Lives { get; set; }
    public bool IsGameStarted { get; set; }

    public event Action<int> OnLiveLost;
 
    private void Start()
    {
        Screen.SetResolution(540, 960, false);
        this.Lives = this.AvailibleLives;         
        Ball.OnBallDeath += OnBallDeath;
        Brick.OnBrickDestruction += OnBrickDestruction;
    }

    private void OnBrickDestruction(Brick obj)
    {
        if (BricksManager.Instance.RemainingBricks.Count <= 0)
        {
            BallManager.Instance.ResetBall();
            GameManager.Instance.IsGameStarted = false;
            BricksManager.Instance.LoadNextLevel();

        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void OnBallDeath(Ball obj)
    {
        if (BallManager.Instance.Ball.Count <=0)
        {
            this.Lives--;

            if (this.Lives < 1)
            {
                gameOverScreen.SetActive(true);
            }
            else
            {             
                OnLiveLost?.Invoke(this.Lives);

                //!!Поставь рестарт мячика
                BallManager.Instance.ResetBall();
                IsGameStarted = false;
                BricksManager.Instance.LoadLevel(BricksManager.Instance.CurrentLevel);
            }
        }
    }

    internal void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }
}
