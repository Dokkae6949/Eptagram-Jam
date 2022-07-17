using Game.StateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies.States
{
    public class IdleState : State
    {
        [SerializeField] private MovementSystem _movement;

        public override void StateEnd()
        {
            
        }

        public override void StateStart()
        {
            _movement.SetInput(Vector2.zero);
        }
        public override State StateUpdate()
        {
            return this;
        }

    }
}
