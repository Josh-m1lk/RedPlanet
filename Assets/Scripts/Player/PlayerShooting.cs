using System;
using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BulletPool bulletPool;
    [SerializeField]Transform bulletSpawn;
    [SerializeField] AmmoUI ammoUI;

    [Header("Settings")]
    [SerializeField] float fireRate = 0f;
    [SerializeField] int maxMag = 0;
    [SerializeField] int reserveAmmo = 0;

    private int currentMag;
    private float nextFireTime;
    //private bool isShooting = false;
    private bool isReloading = false;
    private float reloadDelay = 2;

    void Awake()
    {
        currentMag = maxMag;
        ammoUI.UpdateAmmoUI(currentMag, reserveAmmo);
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
            nextFireTime = Time.time + fireRate;//how long you have to wait in between shots
            
            Bullet bullet = bulletPool.GetBullet();//Get bullet from pool

            //Get bullet spawn pos and rot
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;

            bullet.Fire();//Call fire function after player shoots

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
}
