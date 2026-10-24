using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionAsset playerInput;

    [Header("Script References")]
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] WeaponSwitching weaponSwitching;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;

    private void Awake()
    {
        if (playerShooting == null)
        {
            playerShooting = GetComponent<PlayerShooting>();
        }

        audioSource.clip = audioClip;
    }

    public void OnNextWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerShooting.CancelReload();
            weaponSwitching.SelectWeapon();
            //audioSource.Play();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseMenu.OnPause();
        }
    }

    public void EnableInput()
    {
        playerInput.Enable();
    }

    public void DisableInput()
    {
        playerInput.Disable();
    }
}
