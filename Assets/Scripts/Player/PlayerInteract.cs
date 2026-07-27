using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float detectionRadius = 2f; 
    [SerializeField] LayerMask interactableLayer;
    private Collider[] colliders;
    private int maxColliders = 20;
    private IInteractable focused;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //FindNearestInteractable();
        }
    }

    void Awake()
    {
        colliders = new Collider[maxColliders];
    }

    void Update()
    {
        //IInteractable nearest = FindNearestInteractable();
    } 

    /*private IInteractable FindNearestInteractable()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders, interactableLayer);
        IInteractable nearest = null;
        float bestDistSq = float.MaxValue;

        for (int i = 0; i < hits; i++)
        {
            Collider col = colliders[i];
            if (col == null) continue;
            IInteractable interactable = col.GetComponentInParent<IInteractable>();
            if (interactable == null) continue;
            if (!interactable.CanInteract()) continue;
            float distSq = (col.transform.position - transform.position).sqrMagnitude;
            if (distSq < bestDistSq)
            {
                bestDistSq = distSq;
                nearest = interactable;
            }
        }
    }*/

    void ODrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    //I need to create a collider sphere to detect for colliders 
    //If the sphere does not detect anything continue the function until it does
    //If the player detects something how many? 
    //If player detects something does it have the component I am looking for 
    //if so display the prompt 
}
