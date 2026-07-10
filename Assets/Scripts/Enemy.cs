using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("AI")]
    public GameObject player;
    //private NavMeshAgent enemyAI;
    //public GameObject meleeRadius;
    public float detection = 5f;
    public bool canSeePlayer = false;

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
        //enemyAI = GetComponent<NavMeshAgent>();
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
        //float distance = Vector3.Distance(transform.position, player.transform.position);

        /*if(distance < detection)
        {
            canSeePlayer = true;
            enemyAI.SetDestination(player.transform.position);
        }
        else
        {
            //Debug.Log("Not in range");
        }*/

        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity))
        {
            if (hit.transform == player.transform)
            {
                Debug.Log("I see you");
            }
        }
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);*/
    }

    /*
    Enum enemyStates{

    Walking around
    looking around
    asleep
    chasing the player
    
    }


    DetectPlayer()

    //radius for how close the player and enemy is to activate a raycast
        //enemy curious or cautious
        //maybe it looks around

    //The enemy is curious or cautious


    //when the enemy is cautious create a cone type raycast in front of it


    //if the player is within the raycast of the cone
        //enemy starts chasing the player

    bool PlayerWithinRange = player is in cone

    if (PlayerWithinRange)
    {
        //chase
    }*/
}
