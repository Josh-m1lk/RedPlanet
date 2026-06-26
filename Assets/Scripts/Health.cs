using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float health = 100f;
    private float damageTaken;
    private bool isDead; 

    public PlayerShooting playerShooting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }

    public void Death()
    {
        //If player dies disable controls to player as well as camera
        //If enemy dies destroy the instance of the enemy dead
    }
}
