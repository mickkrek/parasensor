/// <summary>
/// SURGE FRAMEWORK
/// Author: Bob Berkebile
/// Email: bobb@pixelplacement.com
/// 
/// Utilizes script execution order to run before anything else to avoid order of operation failures so accessing things like singletons at any stage of application startup will never fail.
/// 
/// </summary>

using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

namespace Pixelplacement
{
    public class Initialization : MonoBehaviour
    {
        //Private Variables:
        StateMachine _stateMachine;

        //Init:
        void Awake()
        {
            //values:
            _stateMachine = GetComponent<StateMachine>();

            //state machine initialization:
            if (_stateMachine != null) _stateMachine.Initialize();
        }

        void Start()
        {
            //state machine start:
            if (_stateMachine != null) _stateMachine.StartMachine();
        }

        //Deinit:
        void OnDisable()
        {
            if (_stateMachine != null)
            {
                if (!_stateMachine.returnToDefaultOnDisable || _stateMachine.defaultState == null) return;

                if (_stateMachine.currentState == null)
                {
                    _stateMachine.ChangeState(_stateMachine.defaultState);
                    return;
                }

                if (_stateMachine.currentState != _stateMachine.defaultState)
                {
                    _stateMachine.ChangeState(_stateMachine.defaultState);
                }
            }
        }
    }
}