using System;

namespace _Game.Scripts.Interfaces.Health
{
    public interface IHealthComponent
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
        bool IsAlive { get; }
        
        event Action<int, int> OnHealthChanged;
        event Action OnDeath;
        
        void TakeDamage(int damage);
        void Heal(int amount);
        void SetMaxHealth(int maxHealth);
    }
}
