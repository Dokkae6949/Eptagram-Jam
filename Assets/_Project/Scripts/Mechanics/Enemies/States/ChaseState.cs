using UnityEngine;
using Game.StateSystem;
using Game.WeaponSystem;

namespace Game.Enemies.States
{
    public class ChaseState : State
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private MovementSystem _movement;

        private Transform _player;


        public override void StateStart()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public override State StateUpdate()
        {
            Move();

            if (!InRange()) return this;
            if (!CanSee()) return this;

            _movement.SetInput(Vector2.zero);
            return _nextState;
        }

        private void Move()
        {
            Vector2 moveDir = new Vector2(
                _player.position.x - transform.position.x,
                _player.position.y - transform.position.y);
            _movement.SetInput(moveDir.normalized);
        }
        private bool InRange()
        {
            Vector3 difference = new Vector3(
              _player.position.x - transform.position.x,
              _player.position.y - transform.position.y,
              _player.position.z - transform.position.z);
            float sqrDistance =
              Mathf.Pow(difference.x, 2f) +
              Mathf.Pow(difference.y, 2f) +
              Mathf.Pow(difference.z, 2f);

            return sqrDistance <= Mathf.Pow(_weapon.GetMaxBulletRange(), 2);
        }
        private bool CanSee()
        {
            RaycastHit rayResult;
            bool hasHit = Physics.Raycast(
                transform.position,
                _player.position, out rayResult, _weapon.GetMaxBulletRange());

            return hasHit && rayResult.transform.tag == "Player";
        }
    }
}
