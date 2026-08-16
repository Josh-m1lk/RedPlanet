using System.Collections;
using UnityEngine;

public enum GameStates
{
    Playing, 
    Pause,
    GameOver,
    Victory
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStates gameState;
    [SerializeField] LevelManager levelManager;
    [SerializeField] PauseMenu pauseMenu;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gameState = GameStates.Playing;
    }

    public void StartGame()
    {
        gameState = GameStates.Playing;
    }

    public void PauseGame()
    {
        gameState = GameStates.Pause;

        pauseMenu.Pause();
    }

    public void PlayerDied()
    {
        gameState = GameStates.GameOver;

        levelManager.RestartLevel();
    }

    public void LevelComplete()
    {
        gameState = GameStates.Victory;

    }
}
