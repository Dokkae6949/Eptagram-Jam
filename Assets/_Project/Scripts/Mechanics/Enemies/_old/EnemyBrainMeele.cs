using UnityEngine;
using Game.WeaponSystem;

namespace Game.Enemies
{
    [RequireComponent(typeof(Rigidbody), typeof(Weapon))]
    public class EnemyBrainMeele : MonoBehaviour
    {
        [SerializeField] private SOEnemyBrain _brainData;

        [Tooltip("Keeping this empty will result in the player being auto selected")]
        [SerializeField] private Transform _target;

        private Weapon _weapon;
        private Rigidbody _rigidbody;
        private Vector3 _movementInput;


        private void Start()
        {
            if (!_target) _target = GameObject.FindGameObjectWithTag("Player").transform;

            _rigidbody = GetComponent<Rigidbody>();
            _weapon = GetComponent<Weapon>();
        }
        private void Update()
        {
            UpdateMovementInput();
            UpdateShooting();
        }
        private void FixedUpdate()
        {
            UpdateForce();
        }

        public void UpdateMovementInput()
        {
            if (DistanceToTargetSqr() <= _weapon.GetMaxBulletRange() * _weapon.GetMaxBulletRange())
                _movementInput = Vector3.zero;
            else if (_weapon.IsShooting())
                _movementInput = Vector3.zero;

            _movementInput = (_target.position - transform.position).normalized;
        }
        public void UpdateForce()
        {
            if (!_rigidbody) return;

            _rigidbody.AddRelativeForce(_movementInput * _brainData.movementSpeed * _rigidbody.drag);
        }
        public void UpdateShooting()
        {
            if (_weapon == null) return;

            if (DistanceToTargetSqr() <= _weapon.GetMaxBulletRange() * _weapon.GetMaxBulletRange())
                _weapon.StartShooting();
            else _weapon.StopShooting();
        }

        public float DistanceToTargetSqr()
        {
            if (_target == null) return 0;

            return (_target.position - transform.position).sqrMagnitude;
        }
    }
}
