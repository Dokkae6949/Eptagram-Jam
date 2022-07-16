using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.WeaponSystem
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
    public class SOWeapon : ScriptableObject
    {
        public int damage;

        public int damageMultiplier = 1;

        public int shotsPerSecond = 1;

        public float bulletSpray = 0;

        public float maxBulletRange = 100;

        public int bulletsPerShot = 1;
    }
}
