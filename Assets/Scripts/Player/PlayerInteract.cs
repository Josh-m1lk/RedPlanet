using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact();
}

public class PlayerInteract : MonoBehaviour
{
    /*[SerializeField] TextMeshPro interactText;
    private float interactionRange = 0f;
    [SerializeField] LayerMask interactLayer;
    private Collider[] colliders;

    //private IInteractable currentInteractble;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Interact();
        }
    }

    private void Interact()
    {
        //currentInteractble = null;

        int hits = Physics.OverlapSphereNonAlloc(transform.position, interactionRange, colliders, interactLayer);

        foreach (Collider hit in colliders)
        {
            //if (colliders.TryGetComponent(IInteractable))
        }
    }*/
}
