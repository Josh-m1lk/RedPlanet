using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float health;
    public float maxHealth = 100f;
    private float damageTaken;
    private bool isDead; 
    private bool isDamageable;

    public PlayerShooting playerShooting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        //Check to see if player is getting hit by enemy
        //If player takes damage decrease health
        //Check to see if enemy is being hit by bullet
        //If enemy takes damage descrease health
        damageTaken = playerShooting.bulletDamage - health;

        //if 
    }

    public void Death()
    {
        //If player reaches 0 health they die
        //If enemy reaches 0 health they die
    }
}
