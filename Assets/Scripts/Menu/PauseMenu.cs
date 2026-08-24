using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Button resumeGame;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitGame;
    public bool isPaused = false;

    [Header("Script References")]
    [SerializeField] PlayerController playerController;

    void Awake()
    {
        if (pauseScreen != null) pauseScreen.SetActive(false);
    }

    public void ResumeGame()
    {
        playerController.EnableInput();
        if (pauseScreen != null) pauseScreen.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Pause()
    {
        playerController.DisableInput();
        if (pauseScreen != null) pauseScreen.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
