using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [Header("Settings")]
    public float attackDmg = 0f;
    public float attackRange = 0f;
    public bool isAttacking = false;
    private int maxColliders = 1;

    [Header("References")]
    private Collider[] hitColliders;

    [Header("ScriptReferences")]
    [SerializeField] EnemyFOV enemyFOV;
    [SerializeField] EnemyLocomotion enemyLocomotion;

    void Awake()
    {
        hitColliders = new Collider[maxColliders];//initialize array once
    }

    public void Attack(Collider other)
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, attackRange, hitColliders, enemyFOV.targetMask);

        if (hitColliders[0].TryGetComponent(out PlayerHealth health))
        {
            enemyLocomotion.agent.isStopped = true;
            //play animation
            health.TakeDamage(attackDmg);
            isAttacking = true;
        }
    }

    //Make an attack radius to detect if player is in it
    //If player detected in attack range
        //stop ai 
        //hit the player
        //bool turns true
}
