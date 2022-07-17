using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateSystem;

namespace Game.Inventory
{
    public class IdleState : State
    {
        public override void StateEnd()
        {
            
        }

        public override void StateStart()
        {
            
        }
        public override State StateUpdate()
        {
            return this;
        }
    }
}