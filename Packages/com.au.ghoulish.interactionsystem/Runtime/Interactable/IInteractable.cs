using UnityEngine;
namespace Ghoulish.InteractionSystem
{
    public interface IInteractable
    {
        void Interact();
        string GetPromptText();
        Vector3 GetPromptPosition();
    }
}
