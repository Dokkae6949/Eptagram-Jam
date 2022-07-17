using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateSystem;
using Game.WeaponSystem;

namespace Game.Enemies.States
{
    public class ShootState : State
    {
        [SerializeField] private WeaponBasic _weapon;

        public override void StateEnd()
        {
            
        }

        public override void StateStart()
        {
            _weapon.Shoot();
        }
        public override State StateUpdate()
        {
            return _nextState;
        }
    }
}