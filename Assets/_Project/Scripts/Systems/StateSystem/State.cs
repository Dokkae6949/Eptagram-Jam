using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateSystem
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] protected private State _nextState;

        public abstract void StateStart();
        public abstract State StateUpdate();
    }
}
