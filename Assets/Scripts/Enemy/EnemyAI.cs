using Unity.VisualScripting;
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
        enemyLocomotion.GoToNextPatrolPoint();//enemy will go to first patrol point when game starts
    }

    void Update()
    {
        if (enemyFOV.canSeePlayer)
        {
            targetLastSeen = enemyFOV.playerRef.transform.position;
        }
        switch (enemyStates)
        {
            case EnemyStates.Patrol:
                if (enemyFOV.canSeePlayer)
                {
                    //if player is seen in patrol state transition to chase state 
                    enemyStates = EnemyStates.Chase;
                    break;
                }
                
                enemyLocomotion.Patrol();//activate patrol state 
                break;

            case EnemyStates.Investigate:
                if (enemyFOV.canSeePlayer)
                {
                    if (lookCoroutine != null)
                    {
                        //Stop the coroutine and set searching to false
                        StopCoroutine(lookCoroutine);
                        enemyLocomotion.isSearching = false;
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
                    if (enemyFOV.distanceToTarget <= enemyAttack.attackDistance)//if player is close enough
                    {
                        //stop enemy and go to attack state
                        enemyLocomotion.agent.isStopped = true;
                        enemyAttack.isAttacking = true;
                        enemyStates = EnemyStates.Attack;
                    }
                    else
                    {
                        enemyLocomotion.Chase(enemyFOV.playerRef.transform.position);//Go back to chase state if outside of range
                    }
                }
                else
                {
                    enemyStates = EnemyStates.Investigate;
                }
                break;

            case EnemyStates.Attack:
                if (enemyFOV.distanceToTarget > enemyAttack.attackDistance)//if player is outside of attack range
                {
                    //enemy is no longer attacking and goes back to chase
                    enemyLocomotion.agent.isStopped = false;
                    enemyAttack.isAttacking = false;
                    enemyStates = EnemyStates.Chase;

                    break;
                }

                enemyAttack.Attack();
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
