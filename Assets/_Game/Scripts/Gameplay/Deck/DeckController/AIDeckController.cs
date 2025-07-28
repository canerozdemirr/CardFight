using System.Collections.Generic;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Players;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Deck.DeckController
{
    public class AIDeckController : BaseDeckController, IAIDeck
    {
        public bool IsDeckSelected => _cardList.Count >= _totalCardCount;
        public void PrepareDeck()
        {
            Initialize();
        }

        public void AddCardToDeck(Cards.Card card)
        {
            if (!_cardList.Contains(card))
            {
                _cardList.Add(card);
            }
            else
            {
                Debug.LogWarning($"Card {card.name} is already in the deck.");
            }
        }

        public void RemoveCardToDeck(Cards.Card card)
        {
            if (_cardList.Contains(card))
            {
                _cardList.Remove(card);
            }
            else
            {
                Debug.LogWarning($"Card {card.name} not found in the deck.");
            }
        }
    }
}
