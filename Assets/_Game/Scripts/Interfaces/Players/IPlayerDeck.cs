using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Gameplay.Cards;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Interfaces.Players
{
    public interface IPlayerDeck
    {
        bool IsDeckSelected { get; }
        void PrepareDeck();
        void AddCardToDeck(Card card);
        void RemoveCardFromDeck(Card card);
        UniTask ArrangeDeck();
        PlayerOccupation PlayerOccupation { get; }
    }
}
