using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float detectionRadius = 2f; 
    [SerializeField] float lookAngle = 30f;
    [SerializeField] Transform interactionOrigin;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] TextMeshProUGUI interactionPrompt;
    private Collider[] rangeChecks;
    private int maxColliders = 20;
    private IInteractable nearest;
    private IInteractable previousNearest;
    private Camera mainCamera;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            nearest?.Interact();
        }
    }

    void Awake()
    {
        rangeChecks = new Collider[maxColliders];
        mainCamera = Camera.main;
        interactionPrompt.enabled = false;
    }

    void Update()
    {
        nearest = FindNearestInteractable();
        
        if (nearest != previousNearest)
        {
            previousNearest?.OnFocusLose();
            nearest?.OnFocusGain();

            previousNearest = nearest;
        }

        if (nearest != null)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(nearest.InteractPoint.position);

            interactionPrompt.transform.position = screenPos;
            interactionPrompt.text = nearest.InteractionPrompt;
            interactionPrompt.enabled = true;
        }
        else
        {
            interactionPrompt.enabled = false;
        }

    } 

    private IInteractable FindNearestInteractable()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, rangeChecks, interactableLayer);//Sphere to detect interactable colliders
        IInteractable closestInteractable = null;//there is not closest interactable to start
        float closestDistance = Mathf.Infinity;//grab the closes interactable

        for (int i = 0; i < hits; i++)
        {
            Collider hit = rangeChecks[i];//local var for interactables
            Vector3 directionToObject = hit.bounds.center - interactionOrigin.position;//calc for player looking at obj
            float angle = Vector3.Angle(interactionOrigin.forward, directionToObject);//The angle

            if (angle > lookAngle / 2f) continue;

            float distanceToObject = directionToObject.magnitude;

            if (Physics.Raycast(interactionOrigin.position, directionToObject.normalized, out RaycastHit rayHit, distanceToObject))
            {
                IInteractable interactable = rayHit.collider.GetComponentInParent<IInteractable>();

                if (interactable == null) continue;

                float squaredDistance = directionToObject.sqrMagnitude;
                
                if (squaredDistance < closestDistance)
                {
                    closestDistance = squaredDistance;
                    closestInteractable = interactable;
                }
            }
        }
        return closestInteractable;
    }
}
