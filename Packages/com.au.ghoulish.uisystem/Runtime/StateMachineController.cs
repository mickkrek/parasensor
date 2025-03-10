using Pixelplacement;
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    StateMachine stateMachine;
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    public void ChangeState(GameObject state)
    {
        stateMachine.ChangeState(state.name);
    }
}
