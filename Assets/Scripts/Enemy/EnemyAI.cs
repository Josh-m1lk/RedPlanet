using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    enum EnemyStates
    {
        Patrol,
        Investigate,
        Chase,
        Attack
    };

    public NavMeshAgent navMesh;


    [Header("ScriptReferences")]
    public EnemyFOV enemyFOV;
    public EnemyLocomotion enemyMovement;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (enemyFOV.canSeePlayer)
        {
            navMesh.SetDestination(enemyFOV.playerRef.transform.position);
        }
        else
        {
            enemyFOV.canSeePlayer = false;
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
