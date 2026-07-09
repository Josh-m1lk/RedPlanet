using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyFOV : MonoBehaviour
{
    [Header("FovEditor")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public bool canSeePlayer = false;

    [Header("ReferencedObjects")]
    public GameObject playerRef;

    [Header("LayerMasks")]
    public LayerMask target;
    public LayerMask obtruction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FieldOfView()
    {
        
    }
}
