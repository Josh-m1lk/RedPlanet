using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] int startBullets = 0;
    [SerializeField] int maxBullets = 0;

    public ObjectPool<Bullet> bulletPool;

    void Awake()
    {
        //Initialize a new bullet with directions on what to do 
        bulletPool = new ObjectPool<Bullet>(
            OnCreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            true, 
            startBullets,
            maxBullets
            );

        //For loop is ment to create the start bullets on awake 
        for (int i = 0; i < startBullets; i++)
        {
            Bullet bullet = bulletPool.Get();
            bulletPool.Release(bullet);
        }
    }

    private Bullet OnCreateBullet()
    {
        //Create the bullet
        Bullet bullet = Instantiate(bulletPrefab, transform);

        bullet.SetPool(this);

        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        //Get bullet from pool
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        //Release bullet back into pool
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            //Destroy any extra bullets in list 
            Destroy(bullet.gameObject);
        }
        
    }

    public Bullet GetBullet()
    {
        return bulletPool.Get();
    }

    public void ReturnBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }
}
