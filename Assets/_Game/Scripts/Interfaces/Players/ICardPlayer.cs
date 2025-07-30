using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Health;

namespace _Game.Scripts.Interfaces.Players
{
    public interface ICardPlayer
    {
        void TakeDamage(int damage);
        void PlayCard(Card card);
        
        event System.Action<ICardPlayer> OnPlayerDeath;
        event System.Action<Card> OnCardPlayed;
    }
}