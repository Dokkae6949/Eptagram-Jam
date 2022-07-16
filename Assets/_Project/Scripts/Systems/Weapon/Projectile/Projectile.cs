using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BrackeysJam.Events;

namespace Game.WeaponSystem
{
    public abstract class Projectile : MonoBehaviour
    {
        #region Fields and Properties

        protected private Transform _origin;
        public void SetOrigin(Transform value)
        {
            _origin = value;
        }
        public Transform GetOrigin() => _origin;

        protected private int _damage;
        public void SetDamage(int value)
        {
            _damage = Mathf.Clamp(value, 0, int.MaxValue);
        }

        protected private float _speed;
        public void SetSpeed(float value)
        {
            _speed = Mathf.Clamp(value, 0.01f, int.MaxValue);
        }

        protected private LayerMask _damageableLayer;
        public void SetDamageableLayer(LayerMask value)
        {
            _damageableLayer = value;
        }

        private BulletHitEvent _bulletHitEvent;
        public void SetBulletHitEvent(BulletHitEvent value)
        {
            _bulletHitEvent = value;
        }

        protected private float _lifeTime;
        public void SetLifeTime(float value)
        {
            _lifeTime = Mathf.Clamp(value, 0.01f, float.MaxValue);
        }

        #endregion

        private void Start()
        {
            StartCoroutine(LifeTime(_lifeTime));
        }

        public IEnumerator LifeTime(float time)
        {
            yield return new WaitForSeconds(time);

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _origin.parent.gameObject) return;

            BulletHit bulletHit;
            bulletHit.hitPosition = other.ClosestPointOnBounds(transform.position);
            bulletHit.hitObject = other.gameObject;
            bulletHit.bulletOrigin = _origin.gameObject;

            _bulletHitEvent.Raise(bulletHit);

            IDamageable health = other.GetComponent<IDamageable>();

            if (health != null)
                health.DealDamage(_damage, _origin);

            Destroy(gameObject);
        }
    }
}
