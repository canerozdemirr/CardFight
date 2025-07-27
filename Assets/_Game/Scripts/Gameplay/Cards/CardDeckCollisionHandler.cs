using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Gameplay.Deck.DeckSpots;
using _Game.Scripts.Utilities;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Cards
{
    public class CardDeckCollisionHandler : MonoBehaviour
    {
        private readonly List<DeckSpot> _collidingDeckSpots = new List<DeckSpot>();
        
        private DeckSpot _closestDeckSpot;
        private CardDeckState _cardDeckState;
        
        public DeckSpot CollidedDeckSpot => _closestDeckSpot;
        public CardDeckState CardDeckState => _cardDeckState;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out DeckSpot deckSpot))
                return;

            if (!deckSpot.IsSpotEmpty)
                return;
            
            if (!_collidingDeckSpots.Contains(deckSpot))
            {
                _collidingDeckSpots.Add(deckSpot);
                UpdateClosestDeckSpot();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out DeckSpot deckSpot))
                return;
            
            if (_collidingDeckSpots.Contains(deckSpot))
            {
                _collidingDeckSpots.Remove(deckSpot);
                UpdateClosestDeckSpot();
            }
        }
        
        private void UpdateClosestDeckSpot()
        {
            if (_collidingDeckSpots.Count == 0)
            {
                _closestDeckSpot = null;
                _cardDeckState = default;
                return;
            }
            
            Vector2 cardPosition = transform.position;
            DeckSpot closestSpot = _collidingDeckSpots[0];
            float closestDistance = Vector2.Distance(cardPosition, closestSpot.transform.position);

            for (int i = 1; i < _collidingDeckSpots.Count; i++)
            {
                float distance = Vector2.Distance(cardPosition, _collidingDeckSpots[i].transform.position);
                if (!(distance < closestDistance)) 
                    continue;
                
                closestDistance = distance;
                closestSpot = _collidingDeckSpots[i];
            }

            _closestDeckSpot = closestSpot;
            
            UpdateCardDeckState(_closestDeckSpot);
        }
        
        private void UpdateCardDeckState(DeckSpot deckSpot)
        {
            if (deckSpot.gameObject.CompareTag(Constants.BeginningDeckSpotTag))
            {
                _cardDeckState = CardDeckState.InBeginningDeck;
            }
            else if (deckSpot.gameObject.CompareTag(Constants.SelectedDeckSpotTag))
            {
                _cardDeckState = CardDeckState.InSelectingDeck;
            }
            else if (deckSpot.gameObject.CompareTag(Constants.PlayedDeckSpotTag))
            {
                _cardDeckState = CardDeckState.InPlayerDeck;
            }
        }
    }

    public enum CardDeckState
    {
        InPlayerDeck,
        InSelectingDeck,
        InBeginningDeck
    }
}
