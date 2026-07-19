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

    [Header("LayerMasks")]
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer = false;
    
    void Start()
    {
        //playerRef = GameObject.FindGameObjectWithTag("Player");
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
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);//checks for layers in collider range

        if (rangeChecks.Length != 0)//did I detect something
        {
            Transform target = rangeChecks[0].transform;//grab first detected obj
            Vector3 directionToTarget = (target.position - transform.position).normalized;//what direction is the target

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)//is obj inside vision cone
            {
                distanceToTarget = Vector3.Distance(transform.position, target.position);//if so how far 

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))//is there no wall between me and the target
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}
