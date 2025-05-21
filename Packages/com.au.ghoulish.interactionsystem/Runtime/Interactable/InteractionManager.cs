using System.Drawing.Text;
using UnityEngine;
namespace Ghoulish.InteractionSystem
{
    public class InteractionManager : MonoBehaviour
    {
        #region Singleton
        private static InteractionManager _instance;

        private void Awake()
        {
            if (_instance == null || _instance == this)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public static InteractionManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("InteractionManager is NULL");
                return _instance;
            }
        }
        #endregion
        [SerializeField] private float _interactRange = 1f;
        [SerializeField] private LayerMask _interactLayerMask = default;
        [SerializeField] private Transform _playerControllerTransform;
        [SerializeField] private bool InteractionEnabled = true;


        public void OnInteract()
        {
            if (!InteractionEnabled)
            {
                return;
            }
            float nearestDistance = 1000f;
            IInteractable nearestTarget = null;
            Vector3 colliderPosition = _playerControllerTransform.position + (_playerControllerTransform.forward * 0.5f); //move collider position in front of player for more natural interaction
            Collider[] colliderArray = Physics.OverlapSphere(colliderPosition, _interactRange, _interactLayerMask);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out IInteractable target))
                {
                    float thisDistance = Vector3.Distance(colliderPosition, collider.bounds.ClosestPoint(colliderPosition));
                    if (thisDistance < nearestDistance)
                    {
                        nearestDistance = thisDistance;
                        nearestTarget = target;
                    }
                }
            }
            if (nearestTarget != null)
            {
                nearestTarget.Interact();
            }
        }

        public IInteractable GetInteractable()
        {
            if (!InteractionEnabled)
            {
                return null;
            }
            float nearestDistance = 1000f;
            IInteractable nearestTarget = null;
            Vector3 colliderPosition = _playerControllerTransform.position + (_playerControllerTransform.forward * 0.5f); //move collider position in front of player for more natural interaction
            Collider[] colliderArray = Physics.OverlapSphere(colliderPosition, _interactRange, _interactLayerMask);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out IInteractable target))
                {
                    float thisDistance = Vector3.Distance(colliderPosition, collider.bounds.ClosestPoint(colliderPosition));
                    if (thisDistance < nearestDistance)
                    {
                        nearestDistance = thisDistance;
                        nearestTarget = target;
                    }
                }
            }
            if (nearestTarget != null)
            {
                return nearestTarget;
            }
            return null;
        }

        public void EnableInteraction(bool enabled)
        {
            InteractionEnabled = enabled;
        }
    }
}