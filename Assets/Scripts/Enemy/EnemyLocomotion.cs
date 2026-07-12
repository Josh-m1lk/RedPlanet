using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour
{
    [Header("References")]
    private NavMeshAgent agent;

    public bool hasReachedLastKnownPosition;
    public bool isSearching;
    public bool hasFinishedSearching;

    private Quaternion leftRotation;
    private Quaternion rightRotation;
    private Quaternion startRotation;
    private float turnSpeed = 5f;
    private float rotationThreshold = 0.1f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Patrol()
    {
        
    }

    public void Chase(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    public void Investigate(Vector3 targetLastSeen)
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            hasReachedLastKnownPosition = true;
        }
        else
        {
            hasReachedLastKnownPosition = false;
        }

    }

    public IEnumerator LookAround()
    {
        hasFinishedSearching = false;
        isSearching = true;

        float lookAngle = 45f;//lookangle is 45 degrees 
        float searching = 3f;
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
}
