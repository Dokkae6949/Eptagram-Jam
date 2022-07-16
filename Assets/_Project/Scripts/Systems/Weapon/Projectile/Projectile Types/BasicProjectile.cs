using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.WeaponSystem
{
    [RequireComponent(typeof(Collider))]
    public class BasicProjectile : Projectile
    {
        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}
