using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float currentHealth;
    public float maxHealth = 100f;
    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int dmgAmount)
    {
        if (isDead) return;//if alr dead dont do this

        currentHealth -= dmgAmount;//subtract from health
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
