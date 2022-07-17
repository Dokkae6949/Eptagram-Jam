using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.WeaponSystem
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class GranadeProjectile : Projectile
    {
        [SerializeField][Range(0f, 2f)] private float _verticalLaunchBoost;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField][Range(0f, 10f)] private float _dragIncreaseValue;
        [SerializeField][Range(0f, 100f)] private float _maxDrag;
        [SerializeField][Range(0f, 100f)] private float _explosionRadius;
        [SerializeField][Range(0f, 100f)] private float _explosionForce;
        [SerializeField][Range(0f, 100f)] private float _explosionVerticalBoost;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            if (_rigidbody == null) return;
            
            _rigidbody.AddRelativeForce(
                Vector3.forward * _rigidbody.drag * _speed * _rigidbody.mass + Vector3.up * _verticalLaunchBoost, 
                ForceMode.Impulse);
        }

        private void Update()
        {
            _rigidbody.drag = Mathf.Clamp(_rigidbody.drag + _dragIncreaseValue * Time.deltaTime, 0, _maxDrag);
        }

        private protected override void OnTriggerEnterCallback(Collider other)
        {
            Collider[] result = Physics.OverlapSphere(transform.position, _explosionRadius, _damageableLayer);

            foreach (var collider in result)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                
                if (!rb) continue;

                rb.AddExplosionForce(
                    _explosionForce,
                    transform.position,
                    _explosionRadius,
                    _explosionVerticalBoost,
                    ForceMode.Impulse);
            }

            Destroy(gameObject);
        }
    }
}
