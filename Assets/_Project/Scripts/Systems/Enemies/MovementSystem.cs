using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementSystem : MonoBehaviour
    {
        #region Fields and Properties

        public bool isActive = true;

        [SerializeField] private float _acceleration;
        [SerializeField] private float _accelerationMultiplier = 1f;

        public void SetAcceleration(float value)
        {
            if (value >= 0f) _acceleration = value;
        }
        public void SetAccelerationMultiplier(float value)
        {
            if (value >= 0f) _accelerationMultiplier = value;
        }
        public float GetAcceleration() => _acceleration;
        public float GetAccelerationMultiplier() => _accelerationMultiplier;

        private Rigidbody _rigidbody;
        private Vector2 _input;

        public void SetInput(Vector2 value)
        {
            _input = value;
        }
        public Vector2 GetInput() => _input;

        #endregion

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            _rigidbody.AddRelativeForce(new Vector3(_input.x, 0, _input.y) * _rigidbody.drag);
        }
    }
}
