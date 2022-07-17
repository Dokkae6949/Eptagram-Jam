using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateSystem;
using Game.WeaponSystem;
using Game.Generic;

namespace Game.Enemies.States
{
    public class AimState : State
    {
        [SerializeField] private State _chaseState;

        [SerializeField] private WeaponBasic _weapon;
        [SerializeField] private float _requiredLockTime;

        [SerializeField] private Animator _animator;

        private Transform _player;
        private float _lockTime;

        public override void StateEnd()
        {
            
        }

        public override void StateStart()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _lockTime = 0f;
            if (_animator)
                _animator.SetTrigger("StartAiming");
        }
        public override State StateUpdate()
        {
            if (!Helper.InRange(transform.position, _player.position, _weapon.GetMaxShootingRange())) return _chaseState;
            if (!Helper.CanSee(transform.position, _player.position, _weapon.GetMaxShootingRange(), "Player")) _lockTime = 0f;

            _lockTime += Time.deltaTime;

            if (_requiredLockTime - _lockTime <= 0f) return _nextState;

            return this;
        }
    }
}