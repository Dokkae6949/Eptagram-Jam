using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrackeysJam.Events;

namespace Game.WeaponSystem
{
    public class Weapon : WeaponBasic
    {
        #region Fields and Properties

        [SerializeField] private Transform _origin;
        [SerializeField] private LayerMask _damageableLayer;
        [SerializeField] private SOWeapon _weaponData;

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

        [SerializeField][Range(1, 500)] private float _maxShootingRange = 100;
        public void SetMaxShootingRange(float value)
        {
            _maxShootingRange = Mathf.Clamp(value, 1, 500);
        }
        override public float GetMaxShootingRange()
        {
            return _maxShootingRange;
        }

        [SerializeField][Range(1, 100)] private int _bulletsPerShot = 1;
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
        public bool IsShooting()
        {
            return _isShooting;
        }

        private float time;
        private float lastShot;

        #endregion


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
            _maxShootingRange = _weaponData.maxBulletRange;
            _bulletsPerShot = _weaponData.bulletsPerShot;
        }
        private void ShootingHandler()
        {
            bool isCooldownOver = (time - lastShot) > (1f / _shotsPerSecond) ? true : false;

            if (_isShooting && isCooldownOver)
            {
                time = 0f;
                lastShot = time;

                for (int i = 0; i < _bulletsPerShot; i++)
                {
                    Shoot();
                }
            }

            time += Time.deltaTime;
        }
        override public void Shoot()
        {
            // Play Audio Source for shot???

            Vector3 spray = new Vector3(
                _origin.right.x * Random.Range(-_bulletSpray, _bulletSpray), 
                _origin.up.y * Random.Range(-_bulletSpray, _bulletSpray), 
                0);

            RaycastHit hit;
            bool hasHit = Physics.Raycast(_origin.position, _origin.forward + spray, out hit, _maxShootingRange, _damageableLayer);
            // Debug.DrawRay(_origin.position, (_origin.forward + spray) * _maxBulletRange, Color.red, .1f);
            
            if (!hasHit) return;

            IDamageable damageable = hit.transform.GetComponent<IDamageable>();

            if (damageable == null) return;

            damageable.DealDamage(_damage * _damageMultiplier, _origin);

            BulletHit bulletHit;
            bulletHit.hitPosition = hit.point;
            bulletHit.bulletOrigin = _origin.gameObject;
            bulletHit.hitObject = hit.transform.gameObject;
            bulletHit.type = BulletHit.Type.Ray;

            _weaponData._bulletHitEvent.Raise(bulletHit);
        }
    }
}