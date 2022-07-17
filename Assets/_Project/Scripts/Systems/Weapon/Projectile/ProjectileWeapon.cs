using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrackeysJam.Events;

namespace Game.WeaponSystem
{
    public class ProjectileWeapon : WeaponBasic
    {
        #region Fields and Properties

        [SerializeField] private Transform _origin;
        [SerializeField] private LayerMask _damageableLayer;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private BulletHitEvent _bulletHitEvent;

        [SerializeField][Range(0, 1000)] private int _damage;
        public void SetDamage(int value)
        {
            _damage = Mathf.Clamp(value, 0, 1000);
        }

        [SerializeField][Range(1, 200)] private int _damageMultiplier = 1;
        public void SetDamageMultiplier(int value)
        {
            _damageMultiplier = Mathf.Clamp(value, 1, 200);
        }

        [SerializeField][Range(0.01f, 500f)] private float _speed = 5;
        public void SetBulletSpeed(float value)
        {
            _speed = Mathf.Clamp(value, 0.0f, 500f);
        }

        [SerializeField][Range(0.01f, 200)] private float _shotsPerSecond = 1;
        public void SetFireRate(float value)
        {
            _shotsPerSecond = Mathf.Clamp(value, 0.01f, 200);
        }

        [SerializeField][Range(0, 0.2f)] private float _bulletSpray = 0;
        public void SetBulletSpray(float value)
        {
            _bulletSpray = Mathf.Clamp(value, 0, 0.2f);
        }

        [SerializeField][Range(0.01f, 500)] private float _lifeTime = 100;
        public void SetLifeTime(float value)
        {
            _lifeTime = Mathf.Clamp(value, 0.01f, 500);
        }
        public float GetLifeTime() => _lifeTime;

        [SerializeField][Range(0.01f, 500)] private float _maxShootingRange = 100;
        public void SetMaxShootingRange(float value)
        {
            _maxShootingRange = Mathf.Clamp(value, 0.01f, 500);
        }
        override public float GetMaxShootingRange() => _maxShootingRange;

        [SerializeField][Range(1, 100)] private int _bulletsPerShot = 1;
        public void SetBulletsPerShot(int value)
        {
            _bulletsPerShot = Mathf.Clamp(value, 1, 100);
        }


        [ReadOnly]
        [SerializeField]
        private bool _isShooting = false;
        public void StartShooting()
        {
            _isShooting = true;
        }
        public void StopShooting()
        {
            _isShooting = false;
        }
        public bool IsShooting()
        {
            return _isShooting;
        }

        private float lastShot;

        #endregion


        private void Update()
        {
            ShootingHandler();
        }

        private void ShootingHandler()
        {
            bool isCooldownOver = lastShot >= (1f / _shotsPerSecond) ? true : false;

            if (_isShooting && isCooldownOver)
            {
                lastShot = 0f;

                for (int i = 0; i < _bulletsPerShot; i++)
                {
                    Shoot();
                }
            }

            lastShot += Time.deltaTime;
        }
        override public void Shoot()
        {
            // Play Audio Source for shot???

            Quaternion spawnRotation = _origin.transform.rotation;
            Projectile bullet = Instantiate(_projectile, transform.position, spawnRotation);
            
            bullet.SetOrigin(_origin);
            bullet.SetDamage(_damage);
            bullet.SetSpeed(_speed);
            bullet.SetDamageableLayer(_damageableLayer);
            bullet.SetBulletHitEvent(_bulletHitEvent);
            bullet.SetLifeTime(_lifeTime);
        }
    }
}