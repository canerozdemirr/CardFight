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
        private readonly List<DeckSpot> _collidingDeckSpots = new();

        public DeckSpot CollidedDeckSpot { get; private set; }

        public CardDeckState CardDeckState { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out DeckSpot deckSpot))
                return;

            if (!deckSpot.IsSpotEmpty)
                return;

            if (_collidingDeckSpots.Contains(deckSpot)) 
                return;
            
            _collidingDeckSpots.Add(deckSpot);
            UpdateClosestDeckSpot();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out DeckSpot deckSpot))
                return;

            if (!_collidingDeckSpots.Contains(deckSpot)) 
                return;
            
            _collidingDeckSpots.Remove(deckSpot);
            UpdateClosestDeckSpot();
        }
        
        private void UpdateClosestDeckSpot()
        {
            if (_collidingDeckSpots.Count == 0)
            {
                CollidedDeckSpot = null;
                CardDeckState = default;
                return;
            }
            
            Vector2 cardPosition = transform.position;
            DeckSpot closestSpot = _collidingDeckSpots[0];
            float closestDistance = Vector2.Distance(cardPosition, closestSpot.transform.position);

            foreach (DeckSpot deckSpot in _collidingDeckSpots)
            {
                float distance = Vector2.Distance(cardPosition, deckSpot.transform.position);
                if (!(distance < closestDistance)) 
                    continue;
                
                closestDistance = distance;
                closestSpot = deckSpot;
            }

            CollidedDeckSpot = closestSpot;
            
            UpdateCardDeckState(CollidedDeckSpot);
        }
        
        private void UpdateCardDeckState(DeckSpot deckSpot)
        {
            if (deckSpot.gameObject.CompareTag(Constants.BeginningDeckSpotTag))
            {
                CardDeckState = CardDeckState.InBeginningDeck;
            }
            else if (deckSpot.gameObject.CompareTag(Constants.SelectedDeckSpotTag))
            {
                CardDeckState = CardDeckState.InSelectingDeck;
            }
            else if (deckSpot.gameObject.CompareTag(Constants.PlayedDeckSpotTag))
            {
                CardDeckState = CardDeckState.InPlayerDeck;
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
