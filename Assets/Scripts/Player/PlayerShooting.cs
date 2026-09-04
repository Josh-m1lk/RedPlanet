using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BulletPool bulletPool;
    [SerializeField] AmmoUI ammoUI;
    [SerializeField] WeaponSwitching weaponSwitching;

    private float nextFireTime;
    private bool isReloading = false;
    private float reloadDelay = 2f;

    public int currentMag
    {
        get { return weaponSwitching.CurrentWeapon.currentMag; }
        set { weaponSwitching.CurrentWeapon.currentMag = value; }
    }

    public int reserveAmmo
    {
        get { return weaponSwitching.CurrentWeapon.reserveAmmo; }
        set { weaponSwitching.CurrentWeapon.reserveAmmo = value; }
    }

    private void Awake()
    {
        ammoUI.UpdateAmmoUI(currentMag, weaponSwitching.CurrentWeapon.maxMag);
    }

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

    public void Shoot()
    {
        if (isReloading) return;

        if (Time.time > nextFireTime && currentMag > 0)
        {
            WeaponAmmo weapon = weaponSwitching.CurrentWeapon;
            nextFireTime = Time.time + weapon.fireRate;//how long you have to wait in between shots
            
            Bullet bullet = bulletPool.GetBullet();//Get bullet from pool

            //Get bullet spawn pos and rot
            bullet.transform.position = weapon.bulletSpawn.position;
            bullet.transform.rotation = weapon.bulletSpawn.rotation;
            bullet.bulletDamage = weapon.bulletDamage;
            bullet.bulletSpeed = weapon.bulletSpeed;

            bullet.Fire();//Call fire function after player shoots
            SoundManager.EmitSound(weapon.bulletSpawn.position, weapon.gunShotRadius, SoundType.Gunshot);

            currentMag--;//decrease ammo count in mag
            ammoUI.UpdateAmmoUI(currentMag, weaponSwitching.CurrentWeapon.maxMag);//update UI 
        } 
    }  

    public void Reload()
    {
        if (isReloading) return;

        if (currentMag == weaponSwitching.CurrentWeapon.maxMag)
        {
            return;
        }

        isReloading = true;
        StartCoroutine(WaitReloading());
    }

    public void CancelReload()
    {
        if (!isReloading) return;

        StopCoroutine(nameof(WaitReloading));
        isReloading = false;
        ammoUI.UpdateAmmoUI(currentMag, reserveAmmo);
    }

    private IEnumerator WaitReloading()
    {
        ammoUI.ShowReloading();

        yield return new WaitForSeconds(reloadDelay);

        int ammoNeeded = weaponSwitching.CurrentWeapon.maxMag - currentMag;
        int ammoToAdd = Mathf.Min(ammoNeeded, reserveAmmo);

        //Add the amount needed to fill current mag and subtract ammount taken from reserve
        currentMag += ammoToAdd;
        reserveAmmo -= ammoToAdd;

        ammoUI.UpdateAmmoUI(currentMag, weaponSwitching.CurrentWeapon.maxMag);//Update ammo 
        isReloading = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (weaponSwitching != null && weaponSwitching.CurrentWeapon.bulletSpawn != null)
        {
            Gizmos.DrawWireSphere(
                weaponSwitching.CurrentWeapon.bulletSpawn.position,
                weaponSwitching.CurrentWeapon.gunShotRadius
            );
        }
    }
}
