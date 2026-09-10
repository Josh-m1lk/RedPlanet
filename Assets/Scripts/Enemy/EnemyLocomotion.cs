using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    [SerializeField] Transform[] points;
    private Quaternion leftRotation;
    private Quaternion rightRotation;
    private Quaternion startRotation;
    [SerializeField] Animator animator;

    [Header("Settings")]
    private float turnSpeed = 5f;
    private float rotationThreshold = 0.1f;
    private int destinationPoint;
    private float waitTime = 2f;
    private float patrolStoppingDistance = 0.5f;
    private float investigateStoppingDistance = 1f;
    public float attackDistance = 2f;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float investigateSpeed = 3f;
    public bool hasReachedLastKnownPosition = false;
    public bool isSearching = false;
    public bool hasFinishedSearching = false;
    public bool isWaiting = false;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;//disable for continous movement 
    }

    public void ResumeMoving()
    {
        agent.isStopped = false;
    }

    public void StopMoving()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public void SetStoppingDistance(float distance)
    {
        agent.stoppingDistance = distance;
    }

    public void Patrol()
    {
        agent.stoppingDistance = patrolStoppingDistance;

        if (isWaiting)return;//if the enemy is already waiting return nothing 

        if (!agent.pathPending && agent.hasPath && agent.remainingDistance <= patrolStoppingDistance)
        {
            StartCoroutine(WaitAtPatrolPoint());//if distance is no longer being calculated and enemy is close or at start point begin couroutine to go to next
        }
    }

    public void ReturnToPatrolPoint()
    {
        GoToNextPatrolPoint();//call to go back to current patrol point
    }

    public void GoToNextPatrolPoint()
    {
        if (points.Length == 0)return;//return nothing if there are no points

        ResumeMoving();
        agent.SetDestination(points[destinationPoint].position);//go to point 0
        agent.speed = walkSpeed;
        animator.SetBool("IsWalking", true);
    }

    public void Chase(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
        agent.speed = runSpeed;
        animator.SetBool("IsRunning", true);
    }

    public void Investigate(Vector3 destination)
    {
        agent.SetDestination(destination);
        agent.speed = investigateSpeed;
        agent.stoppingDistance = investigateStoppingDistance;

        if (!agent.pathPending && agent.remainingDistance <= investigateStoppingDistance)
        {
            hasReachedLastKnownPosition = true;
            animator.SetBool("IsWalking", false);
        }
        else
        {
            hasReachedLastKnownPosition = false;
        }

    }

    public IEnumerator WaitAtPatrolPoint()
    {
        isWaiting = true;
        StopMoving();
        animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(waitTime);//enemy will wait at the current patrol point for x time

        ResumeMoving();

        destinationPoint = (destinationPoint + 1) % points.Length;//next point in array will become destination
        GoToNextPatrolPoint();

        isWaiting = false;//enemy is no longer waiting because they are moving to next point
    }

    public IEnumerator LookAround()
    {
        //enemy currently searching 
        hasFinishedSearching = false;
        isSearching = true;

        float lookAngle = 45f;//lookangle is 45 degrees 
        float searching = 2.5f;//how long to search
        WaitForSeconds look = new WaitForSeconds(searching);

        //creates left and right rotation, sets the start rotation
        startRotation = transform.rotation;
        leftRotation = startRotation * Quaternion.Euler(0, -lookAngle, 0);
        rightRotation = startRotation * Quaternion.Euler(0, lookAngle, 0);

        //turn left and look 
        yield return StartCoroutine(RotateTo(leftRotation));
        yield return look;

        //turn right and look
        yield return StartCoroutine(RotateTo(rightRotation));
        yield return look;

        //enemy finished searching
        isSearching = false;
        hasFinishedSearching = true;
    }

    public IEnumerator RotateTo(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > rotationThreshold)//while enemy is not close enough to target rotation
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);//calculate rotation towards target and apply new rotation to enemy

            yield return null;//wait one frame before doing it again
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
