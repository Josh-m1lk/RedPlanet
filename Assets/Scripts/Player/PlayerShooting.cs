using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField]Transform bulletSpawn;

    [Header("Settings")]
    [SerializeField] float fireRate = 0f;
    [SerializeField] int ammoCount = 0;

    public ObjectPool<Bullet> objectPool {get; private set;}

    private float nextFireTime;

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Fire();//if left click is pressed perform fire
        }
    }

    public void Fire()
    {
        if (Time.time > nextFireTime && ammoCount > 0)
        {
            nextFireTime = Time.time + fireRate;//how long you have to wait in between shots
            //Create new bullet
            GameObject newBullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);//Create new bullet

            Bullet newBulletScript = newBullet.GetComponent<Bullet>();
            newBulletScript.BulletMovement();
            ammoCount--;
            Destroy(newBullet, 2f);//destroy after 2 seconds
        } 
    }
}
