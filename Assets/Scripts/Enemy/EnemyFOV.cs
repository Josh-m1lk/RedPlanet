using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyFOV : MonoBehaviour
{
    [Header("Settings")]
    public float radius = 0f;
    [Range(0, 360)]
    public float angle = 0f;
    public float distanceToTarget;

    [Header("References")]
    public GameObject playerRef;
    private Collider[] rangeChecks;
    private int maxColliders = 1;

    [Header("LayerMasks")]
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer = false;

    void Awake()
    {
        rangeChecks = new Collider[maxColliders];
    }

    void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    public IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewRoutine();
        }
    }

    private void FieldOfViewRoutine()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, radius, rangeChecks, targetMask);//checks for layers in collider range

        //If enemy does not detect anything in sphere return false
        if (hits == 0)
        {
            ClearTarget();
            return;
        }

        PlayerHealth playerHealth = rangeChecks[0].GetComponentInParent<PlayerHealth>();
        if (playerHealth == null)
        {
            ClearTarget();
            return;
        }

        Transform target = playerHealth.transform;
        Vector3 directionToTarget = (target.position - transform.position).normalized;//what direction is the target

        //If player is outside of vision cone return false
        if (Vector3.Angle(transform.forward, directionToTarget) >= angle / 2)
        {
            ClearTarget();
            return;
        }

        distanceToTarget = Vector3.Distance(transform.position, target.position);//if so how far 

        //if there is a wall between enemy and player, return false
        if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
        {
            ClearTarget();
            return;
        }

        playerRef = playerHealth.gameObject;
        canSeePlayer = true;
    }

    private void ClearTarget()
    {
        playerRef = null;
        canSeePlayer = false;
        distanceToTarget = 0f;
    }
}
