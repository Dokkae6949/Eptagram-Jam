using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateSystem
{
    public class StateManager : MonoBehaviour
    {
        private State _currentState;


        private void Start()
        {
            _currentState.StateStart();
        }
        private void Update()
        {
            UpdateStateMachine();
        }

        private void UpdateStateMachine()
        {
            State nextState = _currentState?.StateUpdate();

            if (nextState != null)
            {
                _currentState = nextState;
                _currentState.StateStart();
            }
        }
    }
}
