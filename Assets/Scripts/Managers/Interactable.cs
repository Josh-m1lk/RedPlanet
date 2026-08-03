using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] Outline outline;
    [SerializeField] UnityEvent onInteract;
    [SerializeField] string interactPrompt;
    [SerializeField] Transform interactionPoint;
    public bool isInteractable = false;

    public string InteractionPrompt => interactPrompt;

    public Transform InteractPoint
    {
        get
        {
            if (interactionPoint != null)
            {
                return interactionPoint;
            }
            return transform;
        }
    }

    void Awake()
    {
        outline = GetComponent<Outline>();
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 1f;
        outline.enabled = false;
    }

    void IInteractable.Interact()
    {
        onInteract?.Invoke();
    }

    void IInteractable.OnFocusGain()
    {
        outline.enabled = true;
        isInteractable = true;
    }

    void IInteractable.OnFocusLose()
    {
        outline.enabled = false;
        isInteractable = false;
    }
}