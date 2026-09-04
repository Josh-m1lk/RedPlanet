using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TerrainTools;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] Button resumeGame;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitGame;
    public bool isPaused = false;

    [Header("Script References")]
    [SerializeField] PlayerController playerController;

    void Awake()
    {
        if (pauseScreen != null) pauseScreen.SetActive(false);
        if (optionsScreen != null) optionsScreen.SetActive(false);
    }

    public void OnResumeGame()
    {
        if (!isPaused) return;
        
        if (pauseScreen != null) pauseScreen.SetActive(false);
        if (optionsScreen != null) optionsScreen.SetActive(false);
        Time.timeScale = 1;

        isPaused = false;

        playerController.EnableInput();
    }

    public void OnPause()
    {
        if (pauseScreen != null) pauseScreen.SetActive(true);
        if (optionsScreen != null)  optionsScreen.SetActive(false);
        Time.timeScale = 0;

        isPaused = true;

        playerController.DisableInput();
    }
    
    public void OnOptions()
    {
        if (pauseScreen != null) pauseScreen.SetActive(false);
        if (optionsScreen != null) optionsScreen.SetActive(true);
    }

    public void OnBackPause()
    {
        if (pauseScreen != null) pauseScreen.SetActive(true);
        if (optionsScreen != null) optionsScreen.SetActive(false);
    }

    public void OnBackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
