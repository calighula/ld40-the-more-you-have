using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonType<GameController>
{
    public float timePerLevel = 10f;

    public GameObject player;

    private UIController uiController;

    private int currentLevel = 4;

    private Vector3 playerStartPosition;

    private int hearts;

    public GameObject[] respawnObjects;

    private void Start()
    {
        playerStartPosition = player.transform.position;
    }

    public void RegisterUIController(UIController newUIController)
    {
        uiController = newUIController;
        uiController.Init(timePerLevel);
    }

    public void PlayerDamaged()
    {
        currentLevel--;
        if (currentLevel > 0)
        {
            // Reset the levels in the UI
            uiController.SetCurrentLevel(currentLevel);
        } else
        {
            // GAMEOVER
            uiController.ShowGameOver(false, 0);
            player.GetComponent<Player>().SetIsGameRunning(false);
        }
    }

    public void LifeLevelCompleted()
    {
        if (currentLevel < 4)
        {
            currentLevel++;
            uiController.SetCurrentLevel(currentLevel);
        }
    }

    public bool IsJumpEnabled()
    {
        return currentLevel <= 3;
    }

    public bool IsFlyEnabled()
    {
        return currentLevel <= 2;
    }

    public bool IsFireEnabled()
    {
        return currentLevel <= 1;
    }

    public void StartNewGame()
    {
        // Set current level
        currentLevel = 4;
        // Reset levels
        uiController.ResetLevels();
        // Set player at the start position
        player.transform.position = playerStartPosition;
        // Enable player input
        player.GetComponent<Player>().SetIsGameRunning(true);
        // Reset number of hearts taken
        hearts = 0;
        // Enable enemies
        if (respawnObjects != null)
        {
            for (int i = 0; i < respawnObjects.Length; i++)
            {
                respawnObjects[i].SetActive(true);
            }
        }
    }

    public void Win()
    {
        uiController.ShowGameOver(true, hearts);
    }

    public void AddHeart()
    {
        hearts++;
    }

}
