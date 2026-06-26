using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
    [Header("GameStates")]
    private bool gameOver;
    private bool startGame;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverState()
    {
        //Controls if the game is over
        //If the player dies with no lives game will end
        //If the player has completed all of the levels the game will also end
    }

    public void StartGameState()
    {
        //Controls if the game has started
        //Create a countdown timer from 3 seconds going down to give player time to prepare
        //If game has started enable player controls after countdown is done
    }
}
