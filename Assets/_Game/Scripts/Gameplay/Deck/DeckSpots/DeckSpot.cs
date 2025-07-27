using _Game.Scripts.Utilities;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Deck.DeckSpots
{
    public class DeckSpot : MonoBehaviour
    {
        private Cards.Card _cardOnSpot;

        public Cards.Card CardOnSpot => _cardOnSpot;
        
        public bool IsSpotEmpty => _cardOnSpot == null;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Cards.Card card) && card.gameObject.CompareTag(Constants.CardTag))
            {
                _cardOnSpot = card;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Cards.Card card) && card.gameObject.CompareTag(Constants.CardTag))
            {
                _cardOnSpot = null;
            }
        }
    }
}
