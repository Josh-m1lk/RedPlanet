using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] BulletPool bulletPool;
    [SerializeField]Transform bulletSpawn;

    [Header("Settings")]
    [SerializeField] float fireRate = 0f;
    [SerializeField] int maxMag = 0;
    [SerializeField] int reserveAmmo = 0;
    private int currentMag;

    [Header("ScriptReferences")]
    [SerializeField] AmmoUI ammoUI;

    private float nextFireTime;

    void Awake()
    {
        currentMag = maxMag;
        ammoUI.UpdateAmmoUI(currentMag, reserveAmmo);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time > nextFireTime && currentMag > 0)
        {
            nextFireTime = Time.time + fireRate;//how long you have to wait in between shots
            
            Bullet bullet = bulletPool.GetBullet();//Get bullet from pool

            //Get bullet spawn pos and rot
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;

            bullet.Fire();//Call fire function after player shoots

            currentMag--;//decrease ammo count in mag
            ammoUI.UpdateAmmoUI(currentMag, reserveAmmo);
        } 
    }
}
