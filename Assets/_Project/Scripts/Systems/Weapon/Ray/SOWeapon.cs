using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrackeysJam.Events;

namespace Game.WeaponSystem
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
    public class SOWeapon : ScriptableObject
    {
        [Range(0, 1000)] public int damage;

        [Range(1, 200)] public int damageMultiplier = 1;

        [Range(0.01f, 200)] public float shotsPerSecond = 1;

        [Range(0.01f, 200)] public float bulletSpray = 0;

        [Range(1, 500)] public float maxBulletRange = 100;

        [Range(1, 100)] public int bulletsPerShot = 1;

        public BulletHitEvent _bulletHitEvent;
    }
}
