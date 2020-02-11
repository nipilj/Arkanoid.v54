using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text LivesText;
    public Text ScoreText;
    public Text FinalScoreText;
    public Text GameOverScoreText;

    public int Score { get; set; }
    public int FinalScore { get; set; }
    public int GameOverScore { get; set; }


    private void Start()
    {
        Brick.OnBrickDestruction += OnBrickDestruction;
        BricksManager.Instance.OnLevelLoaded += OnLevelLoaded;
        GameManager.Instance.OnLiveLost += OnLiveLost;
        OnLiveLost(GameManager.Instance.AvailibleLives);
    }


    private void OnLiveLost(int remainingLives)
    {
        LivesText.text = $"LIVES: {remainingLives}";
    }

    private void OnLevelLoaded()
    {
        UpdateScoreText(0);
        ShowFinalScoreText(0);
        ShowGameOverScoreText(0);
    }

    private void UpdateScoreText(int increment)
    {
        this.Score += increment;
        string scoreString = this.Score.ToString().PadLeft(5, '0');
        ScoreText.text = $@"SCORE:
{scoreString}";
    }

    private void ShowFinalScoreText(int increment)
    {
        this.FinalScore += increment;
        string finalScoreString = this.FinalScore.ToString().PadLeft(5, '0');
        FinalScoreText.text = $@"SCORE: {finalScoreString}";
    }

    private void ShowGameOverScoreText(int increment)
    {
        this.GameOverScore += increment;
        string gameOverScoreString = this.GameOverScore.ToString().PadLeft(5, '0');
        GameOverScoreText.text = $@"SCORE: {gameOverScoreString}";
    }

    private void OnBrickDestruction(Brick obj)
    {
        UpdateScoreText(10);
        ShowFinalScoreText(10);
        ShowGameOverScoreText(10);
    }

    private void OnDisable()
    {
        Brick.OnBrickDestruction -= OnBrickDestruction;
        BricksManager.Instance.OnLevelLoaded -= OnLevelLoaded;
    }
}
