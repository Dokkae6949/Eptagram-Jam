using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Health
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public int Defence { get; private set; }
        public float _defenceModifier;
        public int MaxDefence { get; private set; }

        public UnityEvent OnDeathEvent;


        public void DealDamage(int amount, Transform origin)
        {
            amount -= (int)(Defence * _defenceModifier) ;

            if (amount <= 0) return;

            Health -= amount;
            Health = Mathf.Clamp(Health, 0, MaxHealth);

            if (Health <= 0)
            {
                OnDeath();
            }
        }
        public void Heal(int amount, Transform origin)
        {
            if (amount <= 0) return;

            Health += amount;
            Health = Mathf.Clamp(Health, 0, MaxHealth);
        }

        public void SetDefence(float defence)
        {
            _defenceModifier = defence;
        }

        public void ResetDefence()
        {
            _defenceModifier = 0.5f;
        }

        private void OnDeath()
        {
            OnDeathEvent?.Invoke();
        }
    }
}