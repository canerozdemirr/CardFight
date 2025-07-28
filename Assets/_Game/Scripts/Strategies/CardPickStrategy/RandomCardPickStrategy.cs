using System;
using System.Collections.Generic;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Strategies;

namespace _Game.Scripts.Strategies.CardPickStrategy
{
    [Serializable]
    public class RandomCardPickStrategy : ICardPickStrategy
    {
        public Card PickACard(List<Card> cards)
        {
            if (cards == null || cards.Count == 0)
            {
                throw new Exception("Cannot pick a card from an empty or null list.");
            }

            int randomIndex = UnityEngine.Random.Range(0, cards.Count);
            return cards[randomIndex];
        }
    }
}