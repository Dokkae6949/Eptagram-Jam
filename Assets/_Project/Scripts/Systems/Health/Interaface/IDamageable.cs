using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IDamageable
    {
        void DealDamage(int amount, Transform origin);
        void Heal(int amount, Transform origin);
    }
}