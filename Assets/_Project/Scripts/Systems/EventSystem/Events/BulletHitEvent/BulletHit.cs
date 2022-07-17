using UnityEngine;

namespace BrackeysJam.Events
{
    [System.Serializable] public struct BulletHit 
    {
        public enum Type
        {
            Default = 0,
            Ray = 1,
            Projectile = 2,
            Explosion = 3
        }

        public Type type;
        public Vector3 hitPosition;
        public GameObject hitObject;
        public GameObject bulletOrigin;
    }
}
