using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuSounds : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(UIAudioManager.Instance.PlayButtonClick);
    }
}
