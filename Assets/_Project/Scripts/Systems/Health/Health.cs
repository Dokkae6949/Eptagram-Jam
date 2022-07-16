using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Health
{
    public class Health : MonoBehaviour, IDamageable
    {
        public int health { get; private set; }
        public int maxHealth { get; private set; }

        public int defence { get; private set; }
        public int maxDefence { get; private set; }


        public void DealDamage(int amount)
        {
            amount -= defence;

            if (amount <= 0) return;

            health -= amount;
            health = Mathf.Clamp(health, 0, maxHealth);
        }
        public void Heal(int amount)
        {
            if (amount <= 0) return;

            health += amount;
            health = Mathf.Clamp(health, 0, maxHealth);
        }
    }
}
