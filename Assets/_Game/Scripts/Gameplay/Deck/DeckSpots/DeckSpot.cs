using _Game.Scripts.Utilities;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Deck.DeckSpots
{
    public class DeckSpot : MonoBehaviour
    {
        private Cards.Card _cardOnSpot;
        public bool IsSpotEmpty => _cardOnSpot == null;

        private Collider2D _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!IsSpotEmpty)
                return;
            
            if (other.gameObject.TryGetComponent(out Cards.Card card) && card.gameObject.CompareTag(Constants.CardTag) && card != _cardOnSpot)
            {
                _cardOnSpot = card;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Cards.Card card) && card.gameObject.CompareTag(Constants.CardTag) && card == _cardOnSpot)
            {
                _cardOnSpot = null;
            }
        }

        public void EnableSpot()
        {
            _collider.enabled = true;
        }

        public void DisableSpot()
        {
            _collider.enabled = false;
        }
    }
}
