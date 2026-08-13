using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BulletPool bulletPool;
    [SerializeField]Transform bulletSpawn;
    [SerializeField] AmmoUI ammoUI;

    [Header("Settings")]
    [SerializeField] float fireRate = 0f;
    [SerializeField] int maxMag = 0;
    [SerializeField]public int reserveAmmo = 0;
    [SerializeField]float gunShotRadius = 0f;

    public int currentMag;
    private float nextFireTime;
    private bool isReloading = false;
    private float reloadDelay = 2;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();//If left click is pressed call shoot
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Reload();//When R is pressed call reload
        }
    }

    void Awake()
    {
        currentMag = maxMag;
        ammoUI.UpdateAmmoUI(currentMag, reserveAmmo);
    }

    public void Shoot()
    {
        if (isReloading) return;

        if (Time.time > nextFireTime && currentMag > 0)
        {
            nextFireTime = Time.time + fireRate;//how long you have to wait in between shots
            
            Bullet bullet = bulletPool.GetBullet();//Get bullet from pool

            //Get bullet spawn pos and rot
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;

            bullet.Fire();//Call fire function after player shoots
            SoundManager.EmitSound(bulletSpawn.position, gunShotRadius, SoundType.Gunshot);

            currentMag--;//decrease ammo count in mag
            ammoUI.UpdateAmmoUI(currentMag, reserveAmmo);//update UI 
        } 
    }  

    public void Reload()
    {   
        //If currently reloading and current mag has full ammo don't do nothing
        if (isReloading) return;
        if (currentMag == maxMag) return;

        //Reload will be true and will begin coroutine
        isReloading = true;
        StartCoroutine(WaitReloading());
    }

    public IEnumerator WaitReloading()
    {
        ammoUI.ShowReloading(reserveAmmo);//Show reload 

        yield return new WaitForSeconds(reloadDelay);//Wait for reload to happen 

        int ammoNeeded = maxMag - currentMag;//how much ammo is need for current mag
        int ammoToAdd = Mathf.Min(ammoNeeded, reserveAmmo);//Calculate how much ammo is needed to be taken from reserve

        //Add the amount needed to fill current mag and subtract ammount taken from reserve
        currentMag += ammoToAdd;
        reserveAmmo -= ammoToAdd;

        ammoUI.UpdateAmmoUI(currentMag, reserveAmmo);//Update ammo 
        isReloading = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bulletSpawn.position, gunShotRadius);
    }
}
