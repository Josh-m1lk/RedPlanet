using Unity.VisualScripting;
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
    [SerializeField] AudioSource ambianceSource;
    [SerializeField] AudioClip ambianceClip;

    [Header("OptionsMenu")]
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Slider volume;
    //[SerializeField] AudioMixer masterVolume;
    //[SerializeField] Slider brightness;
    [SerializeField] Button goBack;
    //[SerializeField] AudioSource buttonSource;
    //[SerializeField] AudioClip clickClip;
    

    void Awake()
    {
        if (mainMenu != null) mainMenu.SetActive(true);
        if (optionsMenu != null) optionsMenu.SetActive(false);
        ambianceSource.clip = ambianceClip;
        ambianceSource.Play();
    }

    #region MainMenuFunctions
    
    public void OnStart()
    {
        LevelManager.Instance.LoadNextScene();
        ambianceSource.Stop();
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
