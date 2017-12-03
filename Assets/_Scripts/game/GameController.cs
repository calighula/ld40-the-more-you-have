using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonType<GameController>
{
    public GameObject player;

    private int currentLevel = 4;

    private Vector3 playerStartPosition;

    private void Start()
    {
        playerStartPosition = player.transform.position;
    }

    public void PlayerDamaged()
    {
        currentLevel--;
        if (currentLevel > 0)
        {
            // Reset the levels in the UI
            UIController.Instance.SetCurrentLevel(currentLevel);
        } else
        {
            // GAMEOVER
            UIController.Instance.ShowGameOver();
        }
    }

    public void LifeLevelCompleted()
    {
        if (currentLevel < 4)
        {
            currentLevel++;
            UIController.Instance.SetCurrentLevel(currentLevel);
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
        UIController.Instance.ResetLevels();
        // Set player at the start position
        player.transform.position = playerStartPosition;

        // Enable enemies
        // TODO: Enable all enemies
    }

    public void Poison()
    {

    }

    public void Heal()
    {

    }

}
