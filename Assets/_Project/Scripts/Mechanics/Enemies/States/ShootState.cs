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
        [SerializeField] private Animator _animator;

        public override void StateEnd()
        {
            
        }

        public override void StateStart()
        {
            _weapon.Shoot();

            if (_animator)
                _animator.SetTrigger("StartShooting");
        }
        public override State StateUpdate()
        {
            return _nextState;
        }
    }
}