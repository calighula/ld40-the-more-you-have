using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonType<UIController> {

    public float timePerLevel = 10f;

    public float currentTime = 0f;

    public Image[] levels;

    public GameObject gameOverPanel;

    private int currentLevel = MAX_LEVEL;

    private static int MAX_LEVEL = 4;

    private bool isGameRunning = true;

    // Use this for initialization
    void Start () {
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

    public void ShowGameOver()
    {
        isGameRunning = false;
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].fillAmount = 0;
        }
        gameOverPanel.SetActive(true);
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
