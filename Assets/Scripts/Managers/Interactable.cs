using TMPro;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interactable : MonoBehaviour
{
    [SerializeField] TextMeshPro interactableText;
    
    public void InteractText()
    {
        interactableText.text = "Press E to pick up";
    }
}
