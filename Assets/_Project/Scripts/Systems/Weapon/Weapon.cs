using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private LayerMask _damageableLayer;
        [SerializeField] private SOWeapon _weaponData;

        [SerializeField] private int _damage;
        public void SetDamage(int value)
        {
            if (value > 0) _damage = value;
        }

        [SerializeField] private int _damageMultiplier = 1;
        public void SetDamageMultiplier(int value)
        {
            if (value > 0) _damageMultiplier = value;
        }

        [SerializeField] private int _shotsPerSecond = 1;
        public void SetFireRate(int value)
        {
            if (value > 0) _shotsPerSecond = value;
        }

        [SerializeField] private float _bulletSpray = 0;
        public void SetBulletSpray(float value)
        {
            _bulletSpray = Mathf.Clamp(value, 0, 0.1f);
        }

        [SerializeField] private float _maxBulletRange = 100;
        public void SetMaxBulletRange(float value)
        {
            _maxBulletRange = Mathf.Clamp(value, 1, 500);
        }

        [SerializeField] private int _bulletsPerShot = 1;
        public void SetBulletsPerShot(int value)
        {
            _bulletsPerShot = Mathf.Clamp(value, 1, 100);
        }

        [ReadOnly][SerializeField]
        private bool _isShooting = false;
        public void StartShooting()
        {
            _isShooting = true;
        }
        public void StopShooting()
        {
            _isShooting = false;
        }
        
        private float time;
        private float lastShot;


        private void Start()
        {
            CopySOData();
        }
        private void Update()
        {
            ShootingHandler();
        }


        public void CopySOData()
        {
            if (_weaponData == null) return;

            _damage = _weaponData.damage;
            _damageMultiplier = _weaponData.damageMultiplier;
            _shotsPerSecond = _weaponData.shotsPerSecond;
            _bulletSpray = _weaponData.bulletSpray;
            _maxBulletRange = _weaponData.maxBulletRange;
            _bulletsPerShot = _weaponData.bulletsPerShot;
        }
        private void ShootingHandler()
        {
            bool isCooldownOver = (time - lastShot) > (1f / _shotsPerSecond) ? true : false;

            if (_isShooting && isCooldownOver)
            {
                lastShot = time;

                for (int i = 0; i < _bulletsPerShot; i++)
                {
                    Shoot();
                }
            }

            if (time > 1000000) time = 0f;
            time += Time.deltaTime;
        }
        private void Shoot()
        {
            Vector3 spray = new Vector3(
                _origin.right.x * Random.Range(-_bulletSpray, _bulletSpray), 
                _origin.up.y * Random.Range(-_bulletSpray, _bulletSpray), 
                0);

            RaycastHit hit;
            bool hasHit = Physics.Raycast(_origin.position, _origin.forward + spray, out hit, _maxBulletRange, _damageableLayer);
            // Debug.DrawRay(_origin.position, (_origin.forward + spray) * _maxBulletRange, Color.red, .1f);

            if (!hasHit) return;

            IDamageable damageable = hit.transform.GetComponent<IDamageable>();

            if (damageable == null) return;

            damageable.DealDamage(_damage * _damageMultiplier, _origin);
        }
    }
}