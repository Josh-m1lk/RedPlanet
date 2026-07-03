using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("AI")]
    private PlayerController player;
    private NavMeshAgent enemyAI;
    public float detectionRadius = 2f;
    private Collider[] hitColliders;
    private const int maxColliders = 1;
    public LayerMask playerLayer;

    [Header("Animations")]
    public Animator meleeAnim;
    //private string meleeAnimation = "MeleeAnimation";
    public bool isAttacking = false;

    void Awake()
    {
        hitColliders = new Collider[maxColliders];
        enemyAI = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        meleeAnim = GetComponent<Animator>();
    }
    
    void Update()
    {
        //PlayerHitDetect();
        PlayerDetect();
    }

    void PlayerHitDetect()
    {
        int detectionHit = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, hitColliders, playerLayer);

        //for (int i = 0; i < detectionHit; i++)
            /*if (hitColliders[i].TryGetComponent<PlayerController>(out PlayerController player))//redfine no need for hitcolliders
            {
                meleeAnim.Play(meleeAnimation);
                isAttacking = true;
            }*/
    }

    void PlayerDetect()
    {
        if (player != null)
        {
            enemyAI.SetDestination(player.transform.position);
        }
    }
}
