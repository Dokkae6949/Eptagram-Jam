using UnityEngine;
using Game.StateSystem;
using Game.WeaponSystem;
using Game.Generic;

namespace Game.Enemies.States
{
    public class ChaseState : State
    {
        [SerializeField] private WeaponBasic _weapon;
        [SerializeField] private MovementSystem _movement;

        private Transform _player;


        public override void StateStart()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public override State StateUpdate()
        {
            Move();
            //Debug.Log("R " + Helper.InRange(transform.position, _player.position, _weapon.GetMaxBulletRange()));
            //Debug.Log("C " + Helper.CanSee(transform.position, _player.position, _weapon.GetMaxBulletRange(), "Player"));
            if (!Helper.InRange(transform.position, _player.position, _weapon.GetMaxShootingRange())) return this;
            if (!Helper.CanSee(transform.position, _player.position, _weapon.GetMaxShootingRange(), "Player")) return this;
            
            _movement.SetInput(Vector2.zero);
            return _nextState;
        }

        private void Move()
        {
            Vector2 moveDir = new Vector2(
                _player.position.x - transform.position.x,
                _player.position.z - transform.position.z);
            _movement.SetInput(moveDir.normalized);
        }
    }
}