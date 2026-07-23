using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image healthBar;

    [Header("Settings")]
    public float currentHealth;
    public float maxHealth = 100f;
    private bool isDead = false;
    
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth(float current, float max)
    {
        healthBar.fillAmount = current / max;
    } 

    public void TakeDamage(float dmgAmount)
    {
        if (isDead) return;//if alr dead don't do anything

        currentHealth -= dmgAmount;//subtract from current health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);//clamp health to 0 so to not go below
        
        UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Death();//if equals to 0 or less trigger death()
        }
    }

    public void Death()
    {
        isDead = true;
        Destroy(gameObject);//If enemy reaches 0 health they die

        //GameManager.Instance.PlayerDied();
    }
}
