using System.Collections.Generic;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Gameplay.Cards;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Interfaces.Players
{
    public interface IAIDeck
    {
        bool IsDeckSelected { get; }

        void PrepareDeck();
        void AddCardToDeck(Card card);
        PlayerTurnData PlayerTurnData { get; }
    }
}
