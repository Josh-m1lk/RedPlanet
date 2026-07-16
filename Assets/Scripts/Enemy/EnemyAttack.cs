using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public float enemyAttackDmg = 10f;
    public float attackRange = 5f;
    public bool isAttacking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        isAttacking = true;
        
    }
}
