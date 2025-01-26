using Ghoulish.InteractionSystem;
using UnityEngine;

public class BoolPlayerInteractionOnEnable : MonoBehaviour
{
    [SerializeField] private bool _enable = false;

    private void OnEnable()
    {
        GameManager.Instance.CharacterMovementEnabled(_enable);
        InteractionManager.Instance.EnableInteraction(_enable);
    }
}