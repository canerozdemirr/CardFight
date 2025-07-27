using System.Collections.Generic;
using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Factories;
using _Game.Scripts.Interfaces.GameObjects;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck
{
    public class PlayerDeckSpawner : MonoBehaviour, IPlayerDeckSpawner
    {
        [Inject]
        private CardFactory _cardFactory;

        [Inject] 
        private CardListConfig _cardListConfig;
        
        [SerializeField]
        private List<Transform> _cardSpawnPoints;

        private List<Cards.Card> _cardList;
        
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
    }
}
