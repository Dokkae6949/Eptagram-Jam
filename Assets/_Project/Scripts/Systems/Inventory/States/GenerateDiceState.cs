using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateSystem;

namespace Game.Inventory
{
    public class GenerateDiceState : State
    {
        [SerializeField][Range(0, 8)] private int _newDiceAmount;
        [SerializeField] private InventorySystem _inventorySystem;

        public override void StateStart()
        {
        }
        public override State StateUpdate()
        {
            return _nextState;
        }
    }
}
