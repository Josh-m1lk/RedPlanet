using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [Header("MainMenu")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] Button startGame;
    [SerializeField] Button options;
    [SerializeField] Button quitGame;

    [Header("OptionsMenu")]
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Slider volume;
    //[SerializeField] AudioMixer masterVolume;
    //[SerializeField] Slider brightness;
    [SerializeField] Button goBack;

    void Awake()
    {
        if (mainMenu != null) mainMenu.SetActive(true);
        if (optionsMenu != null) optionsMenu.SetActive(false);
    }

    #region MainMenuFunctions
    
    public void OnStart()
    {
        LevelManager.Instance.LoadNextScene();
    }

    public void OnOptions()
    {
        if (mainMenu != null) mainMenu.SetActive(false);
        if (optionsMenu != null) optionsMenu.SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    #endregion

    #region OptionsMenuFunctions
    public void OnBack()
    {
        if (mainMenu != null) mainMenu.SetActive(true);
        if (optionsMenu != null) optionsMenu.SetActive(false);
    }

    public void Volume()
    {
        //Adjust volume slider 
    }

    public void Brightness()
    {
        //Adjust how bright game is 
    }

    #endregion
}
