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
        [SerializeField] private CardDeckCollisionHandler _cardDeckCollisionHandler;
        [SerializeField] private CardMovementHandler _cardMovementHandler;

        private IMemoryPool _memoryPool;
  
        private int _currentAttackPoint;
        private int _currentDefensePoint;
        private int _maxHealthPoint;
        
        private bool _isCardBeingDragged;
        
        public CardDeckCollisionHandler CardDeckCollisionHandler => _cardDeckCollisionHandler;
        public CardData CardData { get; private set; }
        
        public int AttackPoint => _currentAttackPoint;
        public int DefensePoint => _currentDefensePoint;
        
        public void Dispose()
        {
            CardData = null;
        }

        public void OnDespawned()
        {
            CardData = null;
            gameObject.SetActive(false);
        }

        public void OnSpawned(CardData cardData, IMemoryPool memoryPool)
        {
            CardData = new CardData(cardData);
            _cardVisualHandler.InitializeVisuals(CardData);
            _memoryPool = memoryPool;
            
            _currentAttackPoint = CardData.CardAttackData.AttackPoint;
            _currentDefensePoint = CardData.CardAttackData.DefensePoint;

            gameObject.SetActive(true);
            
            _cardVisualHandler.UpdateAttackVisual(_currentAttackPoint);
            _cardVisualHandler.UpdateDefenseVisual(_currentDefensePoint);
        }

        public void ReturnToPool()
        {
            _memoryPool?.Despawn(this);
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
            _cardVisualHandler.UpdateAttackVisual(_currentAttackPoint);
        }
        
        public void AddDefensePoint(int value)
        {
            if (CardData == null) 
                return;
            
            _currentDefensePoint = Mathf.Max(0, _currentDefensePoint + value);
            _cardVisualHandler.UpdateDefenseVisual(_currentDefensePoint);
        }
        
        public void ResetStatsToBase()
        {
            if (CardData == null) 
                return;
            
            _currentAttackPoint = CardData.CardAttackData.AttackPoint;
            _currentDefensePoint = CardData.CardAttackData.DefensePoint;
        }
        
        #endregion
    }
}