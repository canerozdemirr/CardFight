using System.Collections.Generic;
using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Events.Card;
using _Game.Scripts.Gameplay.Deck.DeckSpots;
using _Game.Scripts.Interfaces.Deck;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Deck.DeckBuilders
{
    using Cards;
    using DeckController;
    using Events.Turn;
    using UnityEngine.Serialization;

    public sealed class PlayerDeckBuilder : BaseDeckBuilder, IPlayerDeckBuilder
    {
        [SerializeField]
        private List<DeckSpot> _cardSpawnPoints;
        
        [FormerlySerializedAs("_playerDeckPoints")] 
        [SerializeField]
        private List<DeckSpot> _selectedCardDeckPoints;
        
        [SerializeField]
        private List<DeckSpot> _playedCardDeckPoints;

        [SerializeField] 
        private BaseDeckController _playerDeck;

        public void PreparePlayerDeck()
        {
            _eventBus.SubscribeTo<OnCardDropped>(OnCardDropped);
            SpawnBeginningDeck();
            _playerDeck.PrepareDeck();
        }

        private void SpawnBeginningDeck()
        {
            CardConfig cardConfig;
            _cardList = new List<Card>(_cardListConfig.CardConfigList.Length);
            for (int i = 0; i < _cardListConfig.CardConfigList.Length; i++)
            {
                cardConfig = _cardListConfig.CardConfigList[i];
                Card spawnedCard = _cardFactory.Create(cardConfig.CardData);
                spawnedCard.transform.position = _cardSpawnPoints[i].transform.position;
                _cardList.Add(spawnedCard);
            }
        }

        private void OnDisable()
        {
            _eventBus.UnsubscribeFrom<OnCardDropped>(OnCardDropped);
        }
        
        private void OnCardDropped(ref OnCardDropped eventData)
        {
            CardDeckState previousCardDeckState = eventData.PickedCard.CardDeckCollisionHandler.CardDeckState;
            eventData.PickedCard.DropToDeckSlot();

            switch (eventData.PickedCard.CardDeckCollisionHandler.CardDeckState)
            {
                case CardDeckState.InPlayerDeck:
                    _eventBus.Raise(new OnPlayerCardPickedToPlay(eventData.PickedCard));
                    break;
                case CardDeckState.InSelectingDeck:
                    _playerDeck.AddCardToDeck(eventData.PickedCard);
                    
                    if (_cardList.Contains(eventData.PickedCard))
                        _cardList.Remove(eventData.PickedCard);
                    
                    if (previousCardDeckState == CardDeckState.InPlayerDeck)
                        _eventBus.Raise(new OnPlayerCardRemovedFromPlay(eventData.PickedCard));
                    
                    break;
                case CardDeckState.InBeginningDeck:
                    _playerDeck.RemoveCardFromDeck(eventData.PickedCard);
                    
                    if (!_cardList.Contains(eventData.PickedCard))
                        _cardList.Add(eventData.PickedCard);
                    
                    if (previousCardDeckState == CardDeckState.InPlayerDeck)
                        _eventBus.Raise(new OnPlayerCardRemovedFromPlay(eventData.PickedCard));
                    break;
            }
        }
        
        public void ClearUnusedCards()
        {
            for (int i = _cardList.Count - 1; i >= 0; i--)
            {
                Card unusedCard = _cardList[i];
                unusedCard.ReturnToPool();
            }
            _cardList.Clear();
        }

        public void CloseDeckSlots()
        {
            foreach (DeckSpot deckSpot in _cardSpawnPoints)
            {
                deckSpot.DisableSpot();
                deckSpot.LockSpot();
                deckSpot.gameObject.SetActive(false);
            }

            foreach (DeckSpot deckSpot in _selectedCardDeckPoints)
            {
                deckSpot.DisableSpot();
                deckSpot.LockSpot();
                deckSpot.gameObject.SetActive(false);
            }

            foreach (DeckSpot deckSpot in _playedCardDeckPoints)
            {
                deckSpot.EnableSpot();
            }
        }
    }
}
