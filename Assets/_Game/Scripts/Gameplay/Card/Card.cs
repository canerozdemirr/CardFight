using System;
using _Game.Scripts.Gameplay.Card.Data;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Card
{
    public class Card : MonoBehaviour, IDisposable, IPoolable<CardData, IMemoryPool>
    {
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
        }
    }
}
