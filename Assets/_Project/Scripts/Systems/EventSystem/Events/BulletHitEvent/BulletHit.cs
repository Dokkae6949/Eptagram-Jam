using UnityEngine;

namespace BrackeysJam.Events
{
    [System.Serializable] public struct BulletHit 
    {
        public Vector3 hitPosition;
        public GameObject hitObject;
        public GameObject bulletOrigin;
    }
}
