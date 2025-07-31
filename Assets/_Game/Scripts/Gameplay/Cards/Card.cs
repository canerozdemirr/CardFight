using System;
using _Game.Scripts.Gameplay.Card;
using _Game.Scripts.Gameplay.Card.Data;
using _Game.Scripts.Interfaces.Health;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Cards
{
    using Health;
    using Interfaces.Players;

    public class Card : MonoBehaviour, IDisposable, IPoolable<CardData, IMemoryPool>
    {
        [SerializeField] private CardVisualHandler _cardVisualHandler;
        [SerializeField] private CardDeckCollisionHandler _cardDeckCollisionHandler;
        [SerializeField] private CardMovementHandler _cardMovementHandler;

        private IMemoryPool _memoryPool;
        private HealthComponent _healthComponent;
        
        private bool _isCardBeingDragged;
        
        public CardDeckCollisionHandler CardDeckCollisionHandler => _cardDeckCollisionHandler;
        public CardData CardData { get; private set; }
        public IHealthComponent Health => _healthComponent;
        public int AttackPoint => CardData?.CardAttackData.AttackPoint ?? 0;
        public int HealthPoint => CardData?.CardAttackData.Health ?? 0;
        public int DefensePoint => CardData?.CardAttackData.DefensePoint ?? 0;

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
            _healthComponent = new HealthComponent(CardData.CardAttackData.Health);
            gameObject.SetActive(true);
            InitializeCard();
        }

        public void ReturnToPool()
        {
            _memoryPool?.Despawn(this);
        }

        private void InitializeCard()
        {
            if (CardData == null)
                return;
            
            _cardVisualHandler.InitializeVisuals(CardData);
        }

        public void DropToDeckSlot()
        {
            _cardDeckCollisionHandler.DropCard();
        }
    }
}