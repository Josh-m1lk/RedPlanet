using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private BulletPool bulletPool;

    [Header("Settings")]
    public float bulletDamage = 0;
    public float bulletSpeed = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetPool(BulletPool pool)
    {
        bulletPool = pool;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("I hit: " + other.gameObject);
        if (other.TryGetComponent(out EnemyHealth health))
        {
            health.TakeDamage(bulletDamage);//do damage to obj
        }

        bulletPool.ReturnBullet(this);//Return bullet to pool
    }

    public void Fire()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;//if rb exists spawn bullet moving at set speed
        }
    }
}
