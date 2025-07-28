using System.Collections.Generic;
using _Game.Scripts.Gameplay.Cards;

namespace _Game.Scripts.Interfaces.Players
{
    public interface IAIDeck
    {
        bool IsDeckSelected { get; }

        void PrepareDeck();
        void AddCardToDeck(Card card);
        void RemoveCardToDeck(Card card);
    }
}
