using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Gameplay.Deck.DeckSpots;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Cards
{
    public class CardDeckCollisionHandler : MonoBehaviour
    {
        [SerializeField] private CardMovementHandler _cardMovementHandler;
        [SerializeField] private Collider2D _cardCollider;
        
        private DeckSpot _defaultDeckSpot;
        private DeckSpot _currentBestSpot;

        private Card _card;
        
        private List<DeckSpot> _collidingDeckSpots = new List<DeckSpot>();
        
        public DeckSpot CollidedDeckSpot => _currentBestSpot ?? _defaultDeckSpot;
        public DeckSpot DefaultDeckSpot => _defaultDeckSpot;

        private void Awake()
        {
            if (_cardCollider == null)
                _cardCollider = GetComponent<Collider2D>();
                
            if (_cardMovementHandler == null)
                _cardMovementHandler = GetComponent<CardMovementHandler>();

            _card = GetComponent<Card>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out DeckSpot deckSpot))
                return;

            if (_defaultDeckSpot == null)
            {
                if (deckSpot.TryOccupySpot(_card))
                {
                    SetDefaultDeckSpot(deckSpot);
                    _currentBestSpot = _defaultDeckSpot;
                }
            }
            
            _collidingDeckSpots.Add(deckSpot);
            UpdateBestCollisionSpot();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out DeckSpot deckSpot))
                return;

            if (!_collidingDeckSpots.Contains(deckSpot)) 
                return;
            
            _collidingDeckSpots.Remove(deckSpot);
            UpdateBestCollisionSpot();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_cardMovementHandler.IsCardBeingDragged)
            {
                UpdateBestCollisionSpot();
            }
        }

        private void UpdateBestCollisionSpot()
        {
            if (!_collidingDeckSpots.Any())
            {
                _currentBestSpot = null;
                return;
            }

            DeckSpot bestSpot = null;
            float maxOverlap = 0f;

            foreach (DeckSpot spot in _collidingDeckSpots)
            {
                float overlap = spot.GetOverlapPercentage(_cardCollider);
                if (overlap > maxOverlap)
                {
                    maxOverlap = overlap;
                    bestSpot = spot;
                }
            }

            _currentBestSpot = bestSpot;
        }

        private bool CanDropToCurrentSpot()
        {
            DeckSpot targetSpot = CollidedDeckSpot;
            
            if (targetSpot == _defaultDeckSpot)
                return true;
            
            return targetSpot != null && targetSpot.IsSpotEmpty;
        }

        public void DropCard()
        {
            DeckSpot targetSpot = GetValidDropSpot();
            
            if (targetSpot != null)
            {
                transform.position = targetSpot.Position;

                if (targetSpot == _defaultDeckSpot) 
                    return;
                
                if (_defaultDeckSpot != null && _defaultDeckSpot.OccupyingCard == _card)
                {
                    _defaultDeckSpot.FreeSpot();
                }
               
                targetSpot.TryOccupySpot(_card);
                _defaultDeckSpot = targetSpot;
            }
            else
            {
                ReturnToDefaultSpot();
            }
        }

        private DeckSpot GetValidDropSpot()
        {
            DeckSpot targetSpot = CollidedDeckSpot;
          
            if (targetSpot == null)
                return _defaultDeckSpot;
          
            return CanDropToCurrentSpot() ? targetSpot : _defaultDeckSpot;
        }

        private void ReturnToDefaultSpot()
        {
            if (_defaultDeckSpot == null) 
                return;
            
            transform.position = _defaultDeckSpot.Position;
            _currentBestSpot = _defaultDeckSpot;
        }

        private void SetDefaultDeckSpot(DeckSpot deckSpot)
        {
            _defaultDeckSpot = deckSpot;
            if (_currentBestSpot == null)
                _currentBestSpot = deckSpot;
        }

        private float GetCurrentOverlapPercentage()
        {
            if (_currentBestSpot == null || _cardCollider == null)
                return 0f;
                
            return _currentBestSpot.GetOverlapPercentage(_cardCollider);
        }

        public bool IsMostlyOverlapping()
        {
            return GetCurrentOverlapPercentage() > 0.5f; // 50% overlap threshold
        }
    }
}
