using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateSystem
{
    public class StateManager : MonoBehaviour
    {
        #region Fields and Properties

        [SerializeField] private bool _isActive = true;
        [SerializeField] private State _currentState;

        public void SetIsActive(bool value)
        {
            _isActive = value;
        }
        public bool GetIsActive() => _isActive;

        public void SetCurrentState(State value)
        {
            if (value == null) return;
            _currentState = value;
        }
        public State GetCurrentState() => _currentState;

        private bool _hasCalledStart = false;

        #endregion

        
        private void Update()
        {
            if (_isActive)
                UpdateStateMachine();
        }

        private void UpdateStateMachine()
        {
            if (!_hasCalledStart)
            {
                _hasCalledStart = true;
                _currentState?.StateStart();
            }

            State nextState = _currentState?.StateUpdate();

            if (nextState != _currentState) _hasCalledStart = false;
            if (nextState != null)
            {
                _currentState = nextState;
            }
        }
    }
}
