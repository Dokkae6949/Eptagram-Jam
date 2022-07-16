using UnityEngine;

namespace BrackeysJam.Events
{
    [CreateAssetMenu(fileName = "New Bullet Hit Event", menuName = "Game Events/Bullet Hit Event")]
    public class BulletHitEvent : BaseGameEvent<BulletHit>
    {
        public void Raise() => Raise(new BulletHit());
    }
}