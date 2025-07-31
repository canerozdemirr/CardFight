using System;
using _Game.Scripts.Gameplay.Card;
using _Game.Scripts.Gameplay.Card.Data;
using _Game.Scripts.Interfaces.Health;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Cards
{
    using Health;

    public class Card : MonoBehaviour, IDisposable, IPoolable<CardData, IMemoryPool>
    {
        [SerializeField] private CardVisualHandler _cardVisualHandler;
        [SerializeField] private CardDeckCollisionHandler _cardDeckCollisionHandler;
        [SerializeField] private CardMovementHandler _cardMovementHandler;

        private IMemoryPool _memoryPool;
        private HealthComponent _healthComponent;
        
        // Runtime modifiable stats
        private int _currentAttackPoint;
        private int _currentDefensePoint;
        private int _maxHealthPoint;
        
        private bool _isCardBeingDragged;
        
        public CardDeckCollisionHandler CardDeckCollisionHandler => _cardDeckCollisionHandler;
        public CardData CardData { get; private set; }
        public IHealthComponent Health => _healthComponent;
        
        public int AttackPoint => _currentAttackPoint;
        public int DefensePoint => _currentDefensePoint;
        public int CurrentHealthPoint => _healthComponent?.CurrentHealth ?? 0;
        public int MaxHealthPoint => _maxHealthPoint;
        
        public int BaseAttackPoint => CardData?.CardAttackData.AttackPoint ?? 0;
        public int BaseDefensePoint => CardData?.CardAttackData.DefensePoint ?? 0;
        public int BaseHealthPoint => CardData?.CardAttackData.Health ?? 0;

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
            CardData = new CardData(cardData);
            _memoryPool = memoryPool;
            
            _currentAttackPoint = CardData.CardAttackData.AttackPoint;
            _currentDefensePoint = CardData.CardAttackData.DefensePoint;
            _maxHealthPoint = CardData.CardAttackData.Health;
            
            _healthComponent = new HealthComponent(_maxHealthPoint);
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
        
        #region Stat Modification Methods
        
        public void AddAttackPoint(int value)
        {
            if (CardData == null) 
                return;
            
            _currentAttackPoint = Mathf.Max(0, _currentAttackPoint + value);
            _cardVisualHandler.UpdateAttackPointDisplay(_currentAttackPoint);
        }
        
        public void SetAttackPoint(int value)
        {
            if (CardData == null) 
                return;
            
            _currentAttackPoint = Mathf.Max(0, value);
            _cardVisualHandler.UpdateAttackPointDisplay(_currentAttackPoint);
        }
        
        public void AddDefensePoint(int value)
        {
            if (CardData == null) 
                return;
            
            _currentDefensePoint = Mathf.Max(0, _currentDefensePoint + value);
            _cardVisualHandler.UpdateDefensePointDisplay(_currentDefensePoint);
        }
        
        public void SetDefensePoint(int value)
        {
            if (CardData == null) 
                return;
            
            _currentDefensePoint = Mathf.Max(0, value);
            _cardVisualHandler.UpdateDefensePointDisplay(_currentDefensePoint);
        }
        
        public void AddMaxHealthPoint(int value)
        {
            if (CardData == null || _healthComponent == null) 
                return;
            
            _maxHealthPoint = Mathf.Max(1, _maxHealthPoint + value);
            _healthComponent.SetMaxHealth(_maxHealthPoint);
            _cardVisualHandler.UpdateHealthPointDisplay(_healthComponent.CurrentHealth, _maxHealthPoint);
        }
        
        public void SetMaxHealthPoint(int value)
        {
            if (CardData == null || _healthComponent == null) 
                return;
            
            _maxHealthPoint = Mathf.Max(1, value);
            _healthComponent.SetMaxHealth(_maxHealthPoint);
            _cardVisualHandler.UpdateHealthPointDisplay(_healthComponent.CurrentHealth, _maxHealthPoint);
        }
        
        public void Heal(int amount)
        {
            if (_healthComponent == null) 
                return;
            
            _healthComponent.Heal(amount);
            _cardVisualHandler.UpdateHealthPointDisplay(_healthComponent.CurrentHealth, _maxHealthPoint);
        }
        
        public void TakeDamage(int damage)
        {
            if (_healthComponent == null) 
                return;
            
            _healthComponent.TakeDamage(damage);
            _cardVisualHandler.UpdateHealthPointDisplay(_healthComponent.CurrentHealth, _maxHealthPoint);
        }
        
        public void ResetStatsToBase()
        {
            if (CardData == null) 
                return;
            
            _currentAttackPoint = CardData.CardAttackData.AttackPoint;
            _currentDefensePoint = CardData.CardAttackData.DefensePoint;
            _maxHealthPoint = CardData.CardAttackData.Health;
            
            if (_healthComponent != null)
            {
                _healthComponent.SetMaxHealth(_maxHealthPoint);
                _healthComponent.Heal(_maxHealthPoint);
            }
            
            _cardVisualHandler.UpdateAllStatsDisplay(_currentAttackPoint, _currentDefensePoint, _healthComponent.CurrentHealth, _maxHealthPoint);
        }
        
        public bool IsAlive()
        {
            return _healthComponent != null && _healthComponent.CurrentHealth > 0;
        }
        
        #endregion
    }
}