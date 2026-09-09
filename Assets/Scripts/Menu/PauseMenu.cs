using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject playerInterface;
    [SerializeField] GameObject pauseBackground;
    [SerializeField] GameObject objectiveText;
    [SerializeField] Button resumeGame;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitGame;
    [SerializeField] VideoPlayer videoPlayer;
    public bool isPaused = false;

    [Header("Script References")]
    [SerializeField] PlayerController playerController;

    void Awake()
    {
        if (pauseScreen == null || optionsScreen == null || playerInterface == null || playerController == null || pauseBackground == null || objectiveText == null)
        {
            Debug.LogError("PauseMenu is missing one or more required references.", this);
            enabled = false;
            return;
        }

        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer is missing", this);
            enabled = false;
            return;
        }

        pauseScreen.SetActive(false);
        optionsScreen.SetActive(false);
        playerInterface.SetActive(true);
        pauseBackground.SetActive(false);
        objectiveText.SetActive(false);
        videoPlayer.Stop();
    }

    public void OnResumeGame()
    {
        if (!isPaused) return;
        
        pauseScreen.SetActive(false);
        pauseBackground.SetActive(false);
        optionsScreen.SetActive(false);
        objectiveText.SetActive(false);
        playerInterface.SetActive(true);

        Time.timeScale = 1;

        videoPlayer.Stop();

        playerController.EnableInput();

        isPaused = false;
    }

    public void OnPause()
    {
        pauseScreen.SetActive(true);
        pauseBackground.SetActive(true);
        objectiveText.SetActive(true);
        optionsScreen.SetActive(false);
        playerInterface.SetActive(false);

        Time.timeScale = 0;

        videoPlayer.Play();

        isPaused = true;

        playerController.DisableInput();
    }
    
    public void OnOptions()
    {
        pauseScreen.SetActive(false);
        objectiveText.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void OnBackPause()
    {
        pauseScreen.SetActive(true);
        objectiveText.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void OnBackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
