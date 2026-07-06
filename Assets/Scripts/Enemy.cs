using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("AI")]
    private PlayerController player;
    private NavMeshAgent enemyAI;
    public GameObject meleeRadius;
    private float detection;

    //Temporary delete
    /*private Collider[] hitColliders;
    private const int maxColliders = 1;
    public LayerMask playerLayer;*/

    [Header("Animations")]
    //public Animator meleeAnim;
    //private string meleeAnimation = "MeleeAnimation";
    public bool isAttacking = false;

    void Awake()
    {
        //hitColliders = new Collider[maxColliders];
        player = FindAnyObjectByType<PlayerController>(); 
        enemyAI = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        //meleeAnim = GetComponent<Animator>();
    }

    void Update()
    {
        //PlayerHitDetect();
        PlayerDetect();
    }

    void PlayerHitDetect()
    {
        //int detectionHit = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, hitColliders, playerLayer);

        //for (int i = 0; i < detectionHit; i++)
            /*if (hitColliders[i].TryGetComponent<PlayerController>(out PlayerController player))//redfine no need for hitcolliders
            {
                meleeAnim.Play(meleeAnimation);
                isAttacking = true;
            }*/
    }

    void PlayerDetect()
    {
        //Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;//get direction
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        if (Physics.Raycast(transform.position, forward, out RaycastHit hit, detection))
        {
            Debug.Log("Player detected");
            enemyAI.SetDestination(player.transform.position);
        }
        Debug.DrawRay(transform.position, forward, Color.red);
    }
}
