using System;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.Health;
using _Game.Scripts.Gameplay.Health;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Testing
{
    public class MockCardPlayer : ICardPlayer
    {
        private IHealthComponent _health;
        
        public PlayerOccupation PlayerOccupation => PlayerOccupation.Player;
        public IHealthComponent Health => _health;
        
        public event Action<ICardPlayer> OnPlayerDeath;
        public event Action<Card> OnCardPlayed;

        public MockCardPlayer(int maxHealth = 100)
        {
            _health = new HealthComponent(maxHealth);
            _health.OnDeath += () => OnPlayerDeath?.Invoke(this);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        public async UniTask PlayCard()
        {
            // Mock implementation
            await UniTask.Yield();
            OnCardPlayed?.Invoke(null);
        }
    }
}