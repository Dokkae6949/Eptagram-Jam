using Game.Enemies.States;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Game.StateSystem
{
    public class StateManager : MonoBehaviour
    {
        #region Fields and Properties

        [SerializeField] private bool _isActive = true;
        [SerializeField] private State _currentState;
        [SerializeField] private IdleState _idleState;


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
        private bool _hasCalledEnd = false;



        #endregion


        private void Update()
        {
            if (_isActive)
                UpdateStateMachine();
            else if (!_hasCalledEnd)
            {
                _hasCalledEnd = true;
                _currentState?.StateEnd();
            }
        }

        private void UpdateStateMachine()
        {
            if (!_hasCalledStart)
            {
                _hasCalledStart = true;
                _currentState?.StateStart();
            }

            State nextState = _currentState?.StateUpdate();
            if (nextState != _currentState)
            {
                _hasCalledStart = false;
                _hasCalledEnd = false;
            }
            if (nextState != null)
            {
                _currentState = nextState;
            }
        }

        public void SetIdleState()
        {
            _currentState = _idleState;
        }
    }
}
