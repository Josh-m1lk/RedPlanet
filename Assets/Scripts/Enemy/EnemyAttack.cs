using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [Header("Settings")]
    public float attackDmg = 0f;
    public float attackDistance = 0f;//How close player needs to be before attack happens
    private float attackRange = 0f;//How far attack hits
    public bool isAttacking = false;
    private int maxColliders = 1;

    [Header("References")]
    private Collider[] hitColliders;

    [Header("ScriptReferences")]
    [SerializeField] EnemyFOV enemyFOV;

    void Awake()
    {
        hitColliders = new Collider[maxColliders];//initialize array once
    }

    public void Attack()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, attackRange, hitColliders, enemyFOV.targetMask);//Determines how big hit detection sphere is

        if (hits > 0)//Did sphere detect at least 1 collider
        {
            if (hitColliders[0].TryGetComponent(out PlayerHealth health))//does it belong to the thing that has health script
            {
                //play animation, do damage, and set bool to true
                //play animation
                health.TakeDamage(attackDmg);
                isAttacking = true;
            }
        }
        
    }

    //Make an attack radius to detect if player is in it
    //If player detected in attack range
        //stop ai 
        //hit the player
        //bool turns true
}
