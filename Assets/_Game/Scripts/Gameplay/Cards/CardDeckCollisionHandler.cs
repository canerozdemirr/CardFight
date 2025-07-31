using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Gameplay.Deck.DeckSpots;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Cards
{
    using Utilities;

    public class CardDeckCollisionHandler : MonoBehaviour
    {
        [SerializeField] private CardMovementHandler _cardMovementHandler;
        [SerializeField] private Collider2D _cardCollider;

        [SerializeField] private bool _isDraggable;
        public bool IsDraggable => _isDraggable;

        private DeckSpot _defaultDeckSpot;
        private DeckSpot _currentBestSpot;

        private Card _card;

        private readonly List<DeckSpot> _collidingDeckSpots = new();

        private CardDeckState _cardDeckState;
        
        public CardDeckState CardDeckState => _cardDeckState;

        public DeckSpot CollidedDeckSpot => _currentBestSpot ?? _defaultDeckSpot;
        public DeckSpot DefaultDeckSpot => _defaultDeckSpot;

        private void Awake()
        {
            if (_cardCollider == null)
                _cardCollider = GetComponent<Collider2D>();

            if (_cardMovementHandler == null)
                _cardMovementHandler = GetComponent<CardMovementHandler>();

            _card = GetComponent<Card>();
            _cardDeckState = CardDeckState.InBeginningDeck;
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
            
            UpdateCardDeckState();
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
        
        private void UpdateCardDeckState()
        {
            if (_currentBestSpot.gameObject.CompareTag(Constants.BeginningDeckSpotTag))
            {
                _cardDeckState = CardDeckState.InBeginningDeck;
            }
            else if (_currentBestSpot.gameObject.CompareTag(Constants.SelectedDeckSpotTag))
            {
                _cardDeckState = CardDeckState.InSelectingDeck;
            }
            else
            {
                _cardDeckState = CardDeckState.InPlayerDeck;
            }
        }
        
        public void OverrideDefaultDeckSpot(DeckSpot newDefaultSpot)
        {
            _defaultDeckSpot = newDefaultSpot;
            _currentBestSpot = newDefaultSpot;
        }
        
        public void SetDraggable(bool isDraggable)
        {
            _isDraggable = isDraggable;
            _cardCollider.enabled = isDraggable;
        }
    }

    public enum CardDeckState
    {
        InPlayerDeck = 0,
        InSelectingDeck = 1,
        InBeginningDeck = 2
    }
}