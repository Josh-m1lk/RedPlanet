using UnityEngine;

public enum EnemyStates
    {
        Patrol,
        Investigate,
        Chase,
        Attack
    };

public class EnemyAI : MonoBehaviour
{
    private EnemyStates enemyStates; 
    private Coroutine lookCoroutine;
    private Vector3 targetLastSeen;

    [Header("ScriptReferences")]
    [SerializeField] EnemyFOV enemyFOV;
    [SerializeField] EnemyLocomotion enemyLocomotion;

    void Awake()
    {
        enemyFOV = GetComponent<EnemyFOV>();
        enemyLocomotion = GetComponent<EnemyLocomotion>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        switch (enemyStates)
        {
            case EnemyStates.Patrol:
                if (enemyFOV.canSeePlayer)
                {
                    enemyStates = EnemyStates.Chase;
                }
                break;
            case EnemyStates.Investigate:
                if (enemyFOV.canSeePlayer)
                {
                    if (lookCoroutine != null)
                    {
                        StopCoroutine(lookCoroutine);//if enemy sees player stop the coroutine
                        enemyLocomotion.isSearching = false;//set search to false so to not stay true forever
                    }
                    enemyStates = EnemyStates.Chase;//enemy goes into chase state after coroutine is stopped
                    break;
                } 
                enemyLocomotion.Investigate(targetLastSeen);//if enemy cant see player investigate
                if (enemyLocomotion.hasReachedLastKnownPosition && lookCoroutine == null)
                {
                    lookCoroutine = StartCoroutine(enemyLocomotion.LookAround());//if enemy reached the last known pos of player and is not searching start look around
                }
                if (enemyLocomotion.hasFinishedSearching)
                {
                    lookCoroutine = null;
                    enemyStates = EnemyStates.Patrol;//once enemy finishes search go back to patrol
                }
                break;
            case EnemyStates.Chase:
                if (enemyFOV.canSeePlayer)
                {
                    enemyLocomotion.Chase(enemyFOV.playerRef.transform.position);//is enemy can see the player chase them
                }
                else
                {
                    enemyStates = EnemyStates.Investigate;
                }
                break;
            case EnemyStates.Attack:
                break;
        }
    }

    //radius for how close the player and enemy is to activate a raycast
        //enemy curious or cautious
        //maybe it looks around

    //The enemy is curious or cautious


    //when the enemy is cautious create a cone type raycast in front of it


    //if the player is within the raycast of the cone
        //enemy starts chasing the player
}
