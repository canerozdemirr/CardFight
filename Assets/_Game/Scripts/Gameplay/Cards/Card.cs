using System;
using _Game.Scripts.Gameplay.Card;
using _Game.Scripts.Gameplay.Card.Data;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Cards
{
    public class Card : MonoBehaviour, IDisposable, IPoolable<CardData, IMemoryPool>
    {
        [SerializeField] private CardVisualHandler _cardVisualHandler;
        
        private CardData _cardData;

        public void Dispose()
        {
            _cardData = null;
        }

        public void OnDespawned()
        {
            _cardData = null;
            gameObject.SetActive(false);
        }

        public void OnSpawned(CardData cardData, IMemoryPool memoryPool)
        {
            _cardData = cardData;
            InitializeCard();
        }

        private void InitializeCard()
        {
            if (_cardData == null) 
                return;
            
            _cardVisualHandler.InitializeVisuals(_cardData);
        }
    }
}
