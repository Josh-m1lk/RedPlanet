using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshProUGUI interactionPrompt;
    [SerializeField] Outline outline;
    [SerializeField] UnityEvent onInteract;
    private bool isInteractable = false;
    public bool CanInteract() => isInteractable;

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
    }

    void IInteractable.OnFocusLose()
    {
        outline.enabled = false;
    }
}
