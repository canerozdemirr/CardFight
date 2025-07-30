using System;
using System.Collections.Generic;
using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Events.Card;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Gameplay.Deck.DeckSpots;
using _Game.Scripts.Interfaces.Deck;
using _Game.Scripts.Interfaces.GameObjects;
using _Game.Scripts.Interfaces.Players;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck.DeckBuilders
{
    public sealed class PlayerDeckBuilder : BaseDeckBuilder, IPlayerDeckBuilder
    {
        [SerializeField]
        private List<DeckSpot> _cardSpawnPoints;
        
        [SerializeField]
        private List<DeckSpot> _playerDeckPoints;

        [Inject] 
        private IPlayerDeck _playerDeck;

        public void PreparePlayerDeck()
        {
            _eventBus.SubscribeTo<OnCardDropped>(OnCardDropped);
            SpawnBeginningDeck();
            _playerDeck.PrepareDeck();
        }

        private void SpawnBeginningDeck()
        {
            CardConfig cardConfig;
            _cardList = new List<Cards.Card>(_cardListConfig.CardConfigList.Length);
            for (int i = 0; i < _cardListConfig.CardConfigList.Length; i++)
            {
                cardConfig = _cardListConfig.CardConfigList[i];
                Cards.Card spawnedCard = _cardFactory.Create(cardConfig.CardData);
                spawnedCard.transform.position =  _cardSpawnPoints[i].transform.position;
                _cardList.Add(spawnedCard);
            }
        }

        private void OnDisable()
        {
            _eventBus.UnsubscribeFrom<OnCardDropped>(OnCardDropped);
        }
        
        private void OnCardDropped(ref OnCardDropped eventData)
        {
            if (!_cardList.Contains(eventData.PickedCard))
                return;

            eventData.PickedCard.DropToDeckSlot();

            /*switch (eventData.PickedCard.CardDeckCollisionHandler.CardDeckState)
            {
                case CardDeckState.InSelectingDeck:
                    _playerDeck.AddCard(eventData.PickedCard);
                    _cardList.Remove(eventData.PickedCard);
                    break;
                case CardDeckState.InBeginningDeck:
                    _playerDeck.RemoveCard(eventData.PickedCard);
                    _cardList.Add(eventData.PickedCard);
                    break;
                case CardDeckState.InPlayerDeck:
                    break;
            }*/
        }
        
        public void ClearUnusedCards()
        {
            for (int i = _cardList.Count - 1; i >= 0; i--)
            {
                Cards.Card unusedCard = _cardList[i];
                unusedCard.ReturnToPool();
            }
            _cardList.Clear();
        }

        public void CloseDeckSlots()
        {
            foreach (DeckSpot deckSpot in _cardSpawnPoints)
            {
                deckSpot.DisableSpot();
            }

            foreach (DeckSpot deckSpot in _playerDeckPoints)
            {
                deckSpot.DisableSpot();
            }
        }
    }
}
