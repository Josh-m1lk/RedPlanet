using UnityEngine;

public interface IInteractable
{
    void Interact();
    bool CanInteract();
    void OnFocusGain();
    void OnFocusLose();
}
