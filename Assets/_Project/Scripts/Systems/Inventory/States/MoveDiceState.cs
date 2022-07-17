using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateSystem;

namespace Game.Inventory
{
    public class MoveDiceState : State
    {
        public float availTime;
        [SerializeField] private LayerMask _inventorySlotLayer;
        [SerializeField] private InventorySystem _inventorySystem;

        public override void StateEnd()
        {
            
        }

        public override void StateStart()
        {

        }
        public override State StateUpdate()
        {
            availTime -= Time.deltaTime;
            if (availTime <= 0) return _nextState;


            return this;
        }
    }
}
