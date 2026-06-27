using UnityEngine;
using UnityEngine.AI;

public class AiEnemy : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent enemyAI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAI = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyAI.SetDestination(player.transform.position);
    }
}
