using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonClick;

    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayButtonClick()
    {
        if (audioSource != null && buttonClick != null)
        {
            audioSource.PlayOneShot(buttonClick);
        }
    }
}
