using System.Collections.Generic;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Health;

namespace _Game.Scripts.Interfaces.Players
{
    using Cysharp.Threading.Tasks;
    using Gameplay.CardPlayers.Data;

    public interface ICardPlayer
    {
        void TakeDamage(int damage);
        UniTask PlayCard();
        PlayerOccupation PlayerOccupation { get; }
        IHealthComponent Health { get; }
        IReadOnlyList<Card> AllCardsInHand { get; }
        event System.Action<ICardPlayer> OnPlayerDeath;
        event System.Action<Card> OnCardPlayed;
    }
}