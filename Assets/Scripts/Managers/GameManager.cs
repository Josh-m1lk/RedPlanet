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
        if (pauseMenu.isPaused)
        {
            gameState = GameStates.Pause;
        }
        else if (!pauseMenu.isPaused)
        {
            gameState = GameStates.Playing;
        }
        
    }

    public void PlayerDied()
    {
        levelManager.RestartLevel();

        gameState = GameStates.GameOver;
    }

    public void LevelComplete()
    {
        

        gameState = GameStates.Victory;
    }
}
