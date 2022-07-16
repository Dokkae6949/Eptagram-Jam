using UnityEngine;
using Game.WeaponSysten;

namespace Game.Player
{
    public class PlayerControls : MonoBehaviour
    {
        [Header("Movement")]
        [Tooltip("Gets multiplied by the amount of drag to keep the movement snappy")]
        [SerializeField] private float _acceleration;
        public void SetAcceleration(float value)
        {
            if (value >= 0f) _acceleration = value;
        }

        [SerializeField] private float _accelerationMultiplier = 1f;
        public void SetAccelerationMultiplier(float value)
        {
            if (value >= 0f) _accelerationMultiplier = value;
        }

        [Header("Camera")]
        [SerializeField] private Transform cameraAnchor;
        [SerializeField] private Vector2 _mouseSensitivity;
        public void SetMouseSensitivity(Vector2 value)
        {
            _mouseSensitivity = value;
        }
        public Vector2 GetMouseSensitivity()
        {
            return _mouseSensitivity;
        }

        private Rigidbody _rigidbody;
        private Weapon _weapon;
        private Vector2 _movementInput;
        private Vector2 _mouseInput;
        private float xRotationThing; // (° _°)


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _weapon = GetComponent<Weapon>();

            Cursor.lockState = CursorLockMode.Locked;
        }
        private void Update()
        {
            UpdateInput();
            UpdateCamera();
            UpdateWeapon();
        }
        private void FixedUpdate()
        {
            UpdateMovement();
        }


        private void UpdateMovement()
        {
            if (!_rigidbody) return;

            Vector2 moveDir = _movementInput.normalized * _acceleration * _accelerationMultiplier * _rigidbody.drag;

            _rigidbody.AddRelativeForce(new Vector3(moveDir.x, 0, moveDir.y));
        }
        private void UpdateCamera()
        {
            if (!cameraAnchor) return;

            transform.Rotate(Vector3.up * _mouseInput.x * Time.deltaTime);

            xRotationThing -= _mouseInput.y * Time.deltaTime;
            xRotationThing = Mathf.Clamp(xRotationThing, -90, 90);
            cameraAnchor.localRotation = Quaternion.Euler(xRotationThing, 0f, 0f);
        }
        private void UpdateWeapon()
        {
            if (!_weapon) return;

            if (Input.GetKeyDown(KeyCode.Mouse0)) _weapon.StartShooting();
            if (Input.GetKeyUp(KeyCode.Mouse0)) _weapon.StopShooting();
        }
        private void UpdateInput()
        {
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");

            _mouseInput.x = Input.GetAxisRaw("Mouse X") * _mouseSensitivity.x;
            _mouseInput.y = Input.GetAxisRaw("Mouse Y") * _mouseSensitivity.y;
        }
    }
}
