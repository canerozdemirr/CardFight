using System.Collections.Generic;
using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Events.Card;
using _Game.Scripts.Gameplay.Cards;
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
        private List<Transform> _cardSpawnPoints;
        
        [SerializeField]
        private List<Transform> _playerDeckPoints;

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
                spawnedCard.transform.position =  _cardSpawnPoints[i].position;
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

            switch (eventData.PickedCard.CardDeckCollisionHandler.CardDeckState)
            {
                case CardDeckState.InPlayerDeck:
                    break;
                case CardDeckState.InSelectingDeck:
                    _playerDeck.AddCard(eventData.PickedCard);
                    break;
                case CardDeckState.InBeginningDeck:
                    _playerDeck.RemoveCard(eventData.PickedCard);
                    break;
            }
        }
    }
}
