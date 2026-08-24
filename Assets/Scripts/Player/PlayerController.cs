using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionAsset playerInput;

    [Header("Script References")]
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] WeaponSwitching weaponSwitching;
    [SerializeField] PlayerShooting playerShooting;

    private void Awake()
    {
        if (playerShooting == null)
        {
            playerShooting = GetComponent<PlayerShooting>();
        }
    }

    public void OnNextWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerShooting.CancelReload();
            weaponSwitching.SelectWeapon();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseMenu.Pause();
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
