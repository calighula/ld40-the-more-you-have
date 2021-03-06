﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonType<UIController> {

    public Image[] levels;

    public GameObject gameOverPanel;

    public Text endMessage;

    private float timePerLevel = 10f;

    private float currentTime = 0f;

    private int currentLevel = MAX_LEVEL;

    private static int MAX_LEVEL = 4;

    private bool isGameRunning = true;

    private void Start()
    {
        GameController.Instance.RegisterUIController(this);
    }

    // Use this for initialization
    public void Init (float newTimePerLevel) {
        timePerLevel = newTimePerLevel;
        StartCoroutine(LifeWatchdog());
	}

    public void SetCurrentLevel(int newCurrentLevel)
    {
        if (newCurrentLevel < currentLevel && currentLevel < MAX_LEVEL)
        {
            // Player is damaged
            levels[currentLevel].fillAmount = 0f;
        }
        currentLevel = newCurrentLevel;
        currentTime = 0;
        if (newCurrentLevel < MAX_LEVEL)
        {
            levels[currentLevel].fillAmount = 0f;
        }
    }

    public void ShowGameOver(bool win, int hearts)
    {
        isGameRunning = false;
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].fillAmount = 0;
        }
        gameOverPanel.SetActive(true);

        if (win)
            endMessage.text = "Y O U W I N \nHearts: " + hearts;
        else
            endMessage.text = "Game Over";
    }

    public void StartNewGame()
    {
        isGameRunning = true;
        gameOverPanel.SetActive(false);
        GameController.Instance.StartNewGame();
    }

    public void ResetLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].fillAmount = 1;
        }
        currentTime = 0;
        currentLevel = MAX_LEVEL;
    }

    IEnumerator LifeWatchdog()
    {
        while(true)
        {
            if (isGameRunning)
            {
                if (currentLevel < MAX_LEVEL)
                {
                    currentTime += Time.deltaTime;
                    if (currentTime > timePerLevel)
                    {
                        currentTime = 0;
                        levels[currentLevel].fillAmount = 1f;
                        GameController.Instance.LifeLevelCompleted();
                    }
                    else
                    {
                        levels[currentLevel].fillAmount = currentTime / timePerLevel;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
