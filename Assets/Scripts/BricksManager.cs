﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BricksManager : MonoBehaviour
{
    #region Singleton

    private static BricksManager _instance;

    public static BricksManager Instance => _instance;

    public event Action OnLevelLoaded;
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


    private int maxRows = 17;
    public int maxCols = 12;

    private GameObject bricksContainer;
    private float initialBrickSpawnPositionX = -2.1f;
    private float initialBrickSpawnPositionY = 2.1f;
    private float shiftAmount = 0.53f;

    public Brick brickPrefab;

    public Sprite[] Sprites;

    public List<Brick> RemainingBricks { get; set; }
    public List<int[,]> LevelsData { get; set; }
    public int CurrentLevel;


    private void Start()
    {
        this.bricksContainer = new GameObject("BricksContiner");
        this.LevelsData = this.LoadLevelsData();
        this.GenerateBricks();
        this.OnLevelLoaded?.Invoke();     
    }


    public void LoadNextLevel()
    {
        this.CurrentLevel++;

        if(this.CurrentLevel >= this.LevelsData.Count)
        {
            GameManager.Instance.ShowVictoryScreen();
        }
        else
        {
            this.LoadLevel(this.CurrentLevel);
        }
    }


    public void GenerateBricks()
    {
        this.RemainingBricks = new List<Brick>();
        int[,] currentLevelData = this.LevelsData[this.CurrentLevel];
        float currentSpawnX = initialBrickSpawnPositionX;
        float currentSpawnY = initialBrickSpawnPositionY;
        float zShift = 0;


        for (int row = 0; row < this.maxRows; row++) 
        {
            for (int col = 0; col < this.maxCols; col++)
            {
                int brickType = currentLevelData[row, col];

                if (brickType > 0)
                {
                    Brick newBrick = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShift), Quaternion.identity) as Brick;
                    newBrick.Init(bricksContainer.transform, Sprites[brickType - 1], brickType);

                    this.RemainingBricks.Add(newBrick);
                    zShift += 0.0003f;
                }

                currentSpawnX += shiftAmount;
                if (col+1==this.maxCols)
                {
                    currentSpawnX = initialBrickSpawnPositionX;
                }
            }

            currentSpawnY -= shiftAmount;
        }
    }


    public void LoadLevel(int level)
    {
        this.CurrentLevel = level;
        this.ClearRemainigBricks();
        this.GenerateBricks();
    }

    private void ClearRemainigBricks()
    {
        foreach(Brick brick in this.RemainingBricks.ToList())
        {
            Destroy(brick.gameObject);
        }
    }


    private List<int[,]> LoadLevelsData()
    {
        TextAsset text = Resources.Load("levels") as TextAsset;

        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        List<int[,]> levelsData = new List<int[,]>();
        int[,] currentLevel = new int[maxRows, maxCols];
        int currentRow = 0;

        for (int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];

            if (line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < bricks.Length; col++)
                {
                    currentLevel[currentRow, col] = int.Parse(bricks[col]);
                }

                currentRow++;
            }
            else
            {
                currentRow = 0;
                levelsData.Add(currentLevel);
                currentLevel = new int[maxRows, maxCols];
            }
        }       

        return levelsData;
    }    
}
