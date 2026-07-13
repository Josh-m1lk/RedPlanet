using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Transform[] points;

    public bool hasReachedLastKnownPosition = false;
    public bool isSearching = false;
    public bool hasFinishedSearching = false;
    public bool isChasing = false;

    private Quaternion leftRotation;
    private Quaternion rightRotation;
    private Quaternion startRotation;

    private float turnSpeed = 5f;
    private float rotationThreshold = 0.1f;
    private int destinationPoint;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2f;//how far to stop from player 
        agent.autoBraking = false;//disable for continous movement 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
           // Patrol();
        }
    }

    /*public void Patrol()
    {
        if (points.Length == 0)
        {
            return;//return nothing if there are no points
        }

        agent.destination = points[destinationPoint].position;//enemy goes to current selected point

        destinationPoint = (destinationPoint + 1) % points.Length;//next point in array will become destination and will restart if needed
    }*/

    public void Chase(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
        isChasing = true;
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
