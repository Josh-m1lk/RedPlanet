using UnityEditor.ShaderGraph;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Settings")]
    public float attackDmg = 20f;
    public float attackRange = 1.5f;//How far attack hits
    public bool isAttacking = false;
    private int maxColliders = 5;
    private float nextAttackTime;
    [SerializeField] float attackCooldown = 1f;

    [Header("References")]
    private Collider[] hitColliders;
    [SerializeField] Transform attackPoint;
    [SerializeField] Animator animator;

    [Header("ScriptReferences")]
    [SerializeField] EnemyFOV enemyFOV;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hitColliders = new Collider[maxColliders];//initialize array once
    }

    public void Attack()
    {
        if (Time.time < nextAttackTime) return;//Leave func if not enough time has passed

        int hits = Physics.OverlapSphereNonAlloc(transform.position, attackRange, hitColliders, enemyFOV.targetMask);//Determines how big hit detection sphere is
        
        if (hits == 0) return;//If nothing detected return nothing
        Debug.Log("I got detected");
        for (int i = 0; i < hits; i++)
        {
            PlayerHealth playerHealth = hitColliders[i].GetComponentInParent<PlayerHealth>();//Does the coliider have player health
            if (playerHealth)
            {
                animator.SetTrigger("AttackTrigger");
                nextAttackTime = Time.time + attackCooldown;//how fast enemy can attack
                playerHealth.TakeDamage(attackDmg);
                isAttacking = true;
                break;
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void HitFrameReached()
    {
        Debug.Log("I have reached hit frame");
    }
}
