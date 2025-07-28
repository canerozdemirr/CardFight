using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Gameplay.Cards;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Interfaces.Players
{
    public interface IPlayerDeck
    {
        bool IsDeckSelected { get; }
        void PrepareDeck();
        void AddCard(Card card);
        void RemoveCard(Card card);
        UniTask ArrangeDeck();
        
        void PlayCard(Card card);

        PlayerTurnData PlayerTurnData { get; }
        CardPlayerHealthData CardPlayerHealthData { get; }
    }
}
