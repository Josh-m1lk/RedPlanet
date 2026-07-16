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
    [Header("References")]
    private EnemyStates enemyStates; 
    private Coroutine lookCoroutine;
    private Vector3 targetLastSeen;

    [Header("ScriptReferences")]
    [SerializeField] EnemyFOV enemyFOV;
    [SerializeField] EnemyLocomotion enemyLocomotion;
    [SerializeField] EnemyAttack enemyAttack;

    void Awake()
    {
        enemyFOV = GetComponent<EnemyFOV>();
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void Start()
    {
        enemyLocomotion.GoToNextPatrolPoint();
    }

    void Update()
    {
        switch (enemyStates)
        {
            case EnemyStates.Patrol:
                if (enemyFOV.canSeePlayer)
                {
                    enemyStates = EnemyStates.Chase;
                    break;//if player is seen in patrol state transition to chase state 
                }
                
                enemyLocomotion.Patrol();//activate patrol state 
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

                enemyLocomotion.Investigate(enemyFOV.playerRef.transform.position);//if enemy cant see player investigate

                if (enemyLocomotion.hasReachedLastKnownPosition && lookCoroutine == null)
                {
                    lookCoroutine = StartCoroutine(enemyLocomotion.LookAround());//if enemy reached the last known pos of player and is not searching start look around
                }
                if (enemyLocomotion.hasFinishedSearching)
                {
                    lookCoroutine = null;//turn off look around after enemy is done investigating
                    enemyLocomotion.ReturnToPatrolPoint();//call to go back to original point 
                    enemyStates = EnemyStates.Patrol;//once enemy finishes search go back to patrol
                    enemyLocomotion.hasFinishedSearching = false;//make false after enemy goes back to patroling
                }
                break;

            case EnemyStates.Chase:
                if (enemyFOV.canSeePlayer)
                {
                    enemyLocomotion.Chase(enemyFOV.playerRef.transform.position);//is enemy can see the player chase them
                    float distanceToPlayer = Vector3.Distance(transform.position, enemyFOV.playerRef.transform.position);//distance from enemy to player
                    if (distanceToPlayer <= enemyAttack.attackRange)
                    {
                        enemyStates = EnemyStates.Attack;//if the enemy is close enough to player begin attack state
                    }
                    else
                    {
                        enemyLocomotion.Chase(enemyFOV.playerRef.transform.position);
                    }
                }
                else
                {
                    enemyStates = EnemyStates.Investigate;
                }
                break;

            case EnemyStates.Attack:
            //If player is in attack range
                //Enemy will attack
                //bool would become true
            //If player not in attack range 
                //Enemy will go back to chase state
                //Bool would become false
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
