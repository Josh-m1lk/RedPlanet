using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] PlayerShooting playerShooting;
    private int collectedAmmo;

    public void AmmoCollected()
    {
        collectedAmmo += playerShooting.reserveAmmo;
    }
}
