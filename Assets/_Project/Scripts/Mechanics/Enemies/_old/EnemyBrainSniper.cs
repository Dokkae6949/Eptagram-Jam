using UnityEngine;
using Game.WeaponSystem;
using System.Collections;

namespace Game.Enemies
{
    [RequireComponent(typeof(Rigidbody), typeof(Weapon))]
    public class EnemyBrainSniper : MonoBehaviour
    {
        [SerializeField] private SOEnemyBrain _brainData;

        [Tooltip("Keeping this empty will result in the player being auto selected")]
        [SerializeField] private Transform _target;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _lineRendererPosition;

        private Weapon _weapon;
        private Rigidbody _rigidbody;
        private Vector3 _movementInput;
        private float time;


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
            UpdateLaser();
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
            {
                _lineRenderer.gameObject.SetActive(true);

                StartCoroutine("Shoot");
            }
            else
            {
                _weapon.StopShooting();
                _lineRenderer.gameObject.SetActive(false);
            }
        }
        private void UpdateLaser()
        {
            if (_lineRenderer == null) return;
            if (_lineRendererPosition == null) return;
            if (!_lineRenderer.gameObject.activeSelf) return;

            RaycastHit ray;
            bool hasHit = Physics.Raycast(
                transform.position,
                transform.forward,
                out ray, 
                _weapon.GetMaxBulletRange());

            if (!hasHit) _lineRenderer.gameObject.SetActive(false);
            else _lineRenderer.gameObject.SetActive(true);

            _lineRenderer.SetPosition(0, _lineRendererPosition.position);
            _lineRenderer.SetPosition(1, ray.point);
        }

        private IEnumerator Shoot()
        {
            yield return new WaitForSeconds(_brainData.attackChargeupTime);

            _weapon.StartShooting();

            yield break;
        }

        public float DistanceToTargetSqr()
        {
            if (_target == null) return 0;

            return (_target.position - transform.position).sqrMagnitude;
        }
    }
}
