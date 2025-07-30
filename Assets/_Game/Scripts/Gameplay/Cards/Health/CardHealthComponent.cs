using System;
using _Game.Scripts.Gameplay.Card.Data;
using _Game.Scripts.Gameplay.Health;
using _Game.Scripts.Interfaces.Health;

namespace _Game.Scripts.Gameplay.Cards.Health
{
    [Serializable]
    public class CardHealthComponent : IHealthComponent
    {
        private HealthComponent _healthComponent;
        private int _defensePoint;

        public int CurrentHealth => _healthComponent.CurrentHealth;
        public int MaxHealth => _healthComponent.MaxHealth;
        public bool IsAlive => _healthComponent.IsAlive;
        public int DefensePoint => _defensePoint;

        public event Action<int, int> OnHealthChanged
        {
            add => _healthComponent.OnHealthChanged += value;
            remove => _healthComponent.OnHealthChanged -= value;
        }

        public event Action OnDeath
        {
            add => _healthComponent.OnDeath += value;
            remove => _healthComponent.OnDeath -= value;
        }

        public CardHealthComponent(CardAttackData cardAttackData)
        {
            _healthComponent = new HealthComponent(cardAttackData.Health);
            _defensePoint = cardAttackData.DefensePoint;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(0, damage - _defensePoint);
            _healthComponent.TakeDamage(actualDamage);
        }

        public int CalculateOverflowDamage(int incomingDamage)
        {
            int damageAfterDefense = Math.Max(0, incomingDamage - _defensePoint);
            int damageToHealth = Math.Min(damageAfterDefense, CurrentHealth);
            return Math.Max(0, damageAfterDefense - damageToHealth);
        }

        public void Heal(int amount) => _healthComponent.Heal(amount);
        public void SetMaxHealth(int maxHealth) => _healthComponent.SetMaxHealth(maxHealth);
        public void ResetToMaxHealth() => _healthComponent.ResetToMaxHealth();
    }
}
