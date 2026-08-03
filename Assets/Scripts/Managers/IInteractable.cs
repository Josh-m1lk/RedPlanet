using UnityEngine;

public interface IInteractable
{
    string InteractionPrompt { get; }
    Transform InteractPoint { get; }

    void Interact();
    void OnFocusGain();
    void OnFocusLose();
}
