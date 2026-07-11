using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("BulletStats")]
    public int bulletDamage = 0;

    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();//checks to see if obj has health script

        if (health != null)
        {
            health.TakeDamage(bulletDamage);
            Destroy(gameObject);
            //if health exists do dmg and destroy on impact
        }
    }

    void Update()
    {
        
    }

}
