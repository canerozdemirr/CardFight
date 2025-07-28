using System.Collections.Generic;
using _Game.Scripts.Gameplay.Cards;

namespace _Game.Scripts.Interfaces.Strategies
{
    public interface ICardPickStrategy
    {
        Card PickACard(List<Card> cards);
    }
}