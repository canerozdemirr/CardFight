using System;
using _Game.Scripts.Gameplay.Card;
using _Game.Scripts.Gameplay.Card.Data;
using _Game.Scripts.Gameplay.Cards.Health;
using _Game.Scripts.Interfaces.Health;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Cards
{
    public class Card : MonoBehaviour, IDisposable, IPoolable<CardData, IMemoryPool>
    {
        [SerializeField] private CardVisualHandler _cardVisualHandler;
        [SerializeField] private CardDeckCollisionHandler _cardDeckCollisionHandler;

        private IMemoryPool _memoryPool;
        private CardHealthComponent _healthComponent;
        
        public CardDeckCollisionHandler CardDeckCollisionHandler => _cardDeckCollisionHandler;
        public CardData CardData { get; private set; }
        public IHealthComponent Health => _healthComponent;
        public int AttackPoint => CardData?.CardAttackData.AttackPoint ?? 0;

        public void Dispose()
        {
            CardData = null;
            _healthComponent = null;
        }

        public void OnDespawned()
        {
            CardData = null;
            _healthComponent = null;
            gameObject.SetActive(false);
        }

        public void OnSpawned(CardData cardData, IMemoryPool memoryPool)
        {
            CardData = cardData;
            _memoryPool = memoryPool;
            _healthComponent = new CardHealthComponent(CardData.CardAttackData);
            gameObject.SetActive(true);
            InitializeCard();
        }

        public void ReturnToPool()
        {
            _memoryPool?.Despawn(this);
        }

        public int CalculateOverflowDamage(int incomingDamage)
        {
            return _healthComponent?.CalculateOverflowDamage(incomingDamage) ?? 0;
        }

        private void InitializeCard()
        {
            if (CardData == null)
                return;
            
            _cardVisualHandler.InitializeVisuals(CardData);
        }

        public void DropToDeckSlot()
        {
            transform.position = _cardDeckCollisionHandler.CollidedDeckSpot.transform.position;
        }
    }
}