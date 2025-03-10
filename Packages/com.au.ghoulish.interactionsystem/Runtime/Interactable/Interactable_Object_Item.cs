using UnityEngine;
namespace Ghoulish.InteractionSystem
{
    public class Interactable_SimpleExample : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _itemName; 
        [SerializeField] private Transform _promptPosition;

        public Vector3 GetPromptPosition()
        {
            return _promptPosition.position;
        }
        public string GetPromptText()
        {
            return "Interact with " + _itemName;
        }

        public void Interact(Transform interactorTransform)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}