using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Button resumeGame;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitGame;

    void Awake()
    {
        if (pauseScreen != null) pauseScreen.SetActive(false);
    }

    public void ResumeGame()
    {
        if (pauseScreen != null) pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        if (pauseScreen != null) pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
