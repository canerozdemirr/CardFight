using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Events.Card;
using _Game.Scripts.Factories;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.GameObjects;
using GenericEventBus;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck
{
    public class PlayerDeckBuilder : MonoBehaviour, IPlayerDeckSpawner
    {
        [Inject]
        private CardFactory _cardFactory;

        [Inject] 
        private CardListConfig _cardListConfig;

        [Inject] 
        private GenericEventBus<IEvent> _eventBus;
        
        [SerializeField]
        private List<Transform> _cardSpawnPoints;
        
        [SerializeField]
        private List<Transform> _playerDeckPoints;

        private List<Cards.Card> _cardList;

        public void PrepareDeck()
        {
            _eventBus.SubscribeTo<OnCardDropped>(OnCardDropped);
        }

        public void SpawnBeginningDeck()
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

        public void OnDisable()
        {
            _eventBus.UnsubscribeFrom<OnCardDropped>(OnCardDropped);
        }
        
        private void OnCardDropped(ref OnCardDropped eventData)
        {
            if (!_cardList.Contains(eventData.PickedCard))
                return;

            eventData.PickedCard.DropToDeckSlot();
        }
    }
}
