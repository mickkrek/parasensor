using UnityEngine;
namespace Ghoulish.InteractionSystem
{
    public interface IInteractable
    {
        void Interact(Transform interactorTransform);
        string GetPromptText();
        Vector3 GetPromptPosition();
    }
}
