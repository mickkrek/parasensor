using UnityEngine;
using System.Collections;
namespace Ghoulish.PlayerControls
{
    public abstract class IPlayerControls : MonoBehaviour
    {
        [SerializeField] protected float playerSpeed = 2f;
        [SerializeField] protected float movementSmoothSpeed = 0.4f;
        [SerializeField] protected float gravityValue = -1f;
        [SerializeField] protected float controllerDeadzone = 0.01f;
        [HideInInspector] public CharacterController charController;
        protected Vector3 movement = new Vector3(0f,0f,0f);
        protected bool playerCanMove = true;
        protected bool playerIsMoving = false;

        protected virtual void Awake()
        {
            charController = GetComponent<CharacterController>();
        }
        protected virtual void Update()
        {
            if (playerCanMove)
            {
                HandleInput();
            }
        }

        protected virtual void FixedUpdate()
        {
            movement = Vector3.zero;
            HandleMovement();
            ApplyGravity();
            charController.Move(movement);
        }
        protected virtual void ApplyGravity()
        {
            movement.y += gravityValue * Time.deltaTime;
        }
        protected abstract void HandleInput();
        protected abstract void HandleMovement();

        public void MovementEnabled(bool enabled)
        {
            playerCanMove = enabled;
        }

        public IEnumerator NudgePlayer(int duration, float speed, Vector3 direction, float snapForwardAmount, bool changeDirection = true)
        {
            playerCanMove = false;
            direction.y = charController.transform.position.y;
            if (changeDirection) 
            {
                charController.transform.LookAt(direction);
            }
            charController.Move(charController.transform.forward * snapForwardAmount); //snap the player forward by this amount
            float i = 0;
            while (i < duration)
            {
                i += Time.deltaTime;
                charController.Move(charController.transform.forward * speed);
                yield return null;
            }
            playerCanMove = true;
        }
    }
}