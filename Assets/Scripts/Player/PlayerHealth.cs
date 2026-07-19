using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    public float currentHealth;
    public float maxHealth = 100f;
    [SerializeField]private bool isDead = false;
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float dmgAmount)
    {
        if (isDead) return;//if alr dead don't do anything

        currentHealth -= dmgAmount;//subtract from current health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);//clamp health to 0 so to not go below
        if (currentHealth <= 0)
        {
            Death();//if equals to 0 or less trigger death()
        }
    }

    public void Death()
    {
        isDead = true;
        Destroy(gameObject);//If enemy reaches 0 health they die
    }
}
