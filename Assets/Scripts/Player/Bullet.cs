using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;

    [Header("Settings")]
    public float bulletDamage = 0;
    public float bulletSpeed = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth health))
        {
            health.TakeDamage(bulletDamage);
            Destroy(gameObject);
            //if health exists do dmg and destroy on impact
        }
    }

    public void BulletMovement()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;//if rb exists spawn bullet moving at set speed
        }
    }
}
