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
    /*public static GameManager Instance;
    public GameStates gameState;
    [SerializeField] LevelManager levelManager;

    void Awake()
    {
        Instance = this;
        gameState = GameStates.Playing;
    }

    public void StartGame()
    {
        gameState = GameStates.Playing;
    }

    public void PauseGame()
    {
        gameState = GameStates.Pause;
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

    public IEnumerator RestartLevelDelay()
    {
        float restartDelay = 5f;

        yield return new WaitForSeconds(restartDelay);
    }*/
}
