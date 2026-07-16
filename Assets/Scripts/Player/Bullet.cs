using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Rigidbody rb;

    [Header("Settings")]
    public int bulletDamage = 0;
    public float bulletSpeed = 0f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(bulletDamage);
            Destroy(gameObject);
            //if health exists do dmg and destroy on impact
        }
    }

    public void BulletSpawn()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;//if rb exists spawn bullet moving at set speed
        }
    }
}
