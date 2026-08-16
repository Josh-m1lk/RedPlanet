using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] PauseMenu pauseMenu;

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseMenu.Pause();
        }
    }
}
