using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Health
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        public int _health;
        public int _maxHealth;

        public int Defence { get; private set; }
        public float _defenceModifier;
        public int MaxDefence { get; private set; }

        public UnityEvent OnDeathEvent; 
        public UnityEvent OnDamageEvent;


        public void DealDamage(int amount, Transform origin)
        {
            amount -= (int)(Defence * _defenceModifier) ;

            if (amount <= 0) return;

            _health -= amount;
            _health = Mathf.Clamp(_health, 0, _maxHealth);
            OnDamageEvent?.Invoke();
            if (_health <= 0)
            {
                OnDeath();
            }
        }
        public void Heal(int amount, Transform origin)
        {
            if (amount <= 0) return;

            _health += amount;
            _health = Mathf.Clamp(_health, 0, _maxHealth);
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