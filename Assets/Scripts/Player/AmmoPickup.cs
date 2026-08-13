using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] AmmoUI ammoUI;
    public int collectedAmmo;

    void Awake()
    {
        gameObject.SetActive(true);
    }

    public void AmmoCollected()
    {
        playerShooting.reserveAmmo += collectedAmmo;
        ammoUI.UpdateAmmoUI(playerShooting.currentMag, playerShooting.reserveAmmo);
        gameObject.SetActive(false);
    }
}
