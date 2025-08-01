using System;
using UnityEngine;
using _Game.Scripts.Interfaces.Health;

namespace _Game.Scripts.Gameplay.Health
{
    [Serializable]
    public class HealthComponent : IHealthComponent
    {
        private int _currentHealth;
        private int _maxHealth;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public bool IsAlive => _currentHealth > 0;

        public event Action<int, int> OnHealthChanged;
        public event Action OnDeath;

        public HealthComponent(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (!IsAlive) 
                return;

            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (!IsAlive)
            {
                OnDeath?.Invoke();
            }
        }

        public void Heal(int amount)
        {
            _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        public void SetMaxHealth(int maxHealth)
        {
            _maxHealth = Mathf.Max(1, maxHealth);
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        public void ResetToMaxHealth()
        {
            if (_currentHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }
}
