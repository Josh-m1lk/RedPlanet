using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 0f;
    public float fireRate = 0f;
    public float ammoCount = 0f;
    public float bulletDamage = 15f;
    private float canFire;

    private Rigidbody rb;

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Fire();//if left click is pressed perform fire
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();//Get rb on awake
    }

    void Update()
    {
        
    }

    public void Fire()
    {
        if (Time.time > canFire && ammoCount > 0)
        {
            canFire = Time.time + fireRate;//how long you have to wait in between shots
            //Create new bullet and for each new one get rb
            GameObject newBullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);//Create new bullet
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = bulletSpawn.forward * bulletSpeed;//if rb exists spawn bullet moving at set speed
            }
            ammoCount--;
            Destroy(newBullet, 2f);//destroy after 2 seconds
        } 
    }
}
