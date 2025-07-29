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

        private CardData _cardData;
        private IMemoryPool _memoryPool;
        private int _currentHealth;
        
        public CardDeckCollisionHandler CardDeckCollisionHandler => _cardDeckCollisionHandler;
        public CardData CardData => _cardData;
        public int CurrentHealth => _currentHealth;
        public int AttackPoint => _cardData?.CardAttackData.AttackPoint ?? 0;
        public int DefensePoint => _cardData?.CardAttackData.DefensePoint ?? 0;
        public bool IsDestroyed => _currentHealth <= 0;

        public void TakeDamage(int damage)
        {
            if (damage <= 0) return;
            
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            
            if (_currentHealth <= 0)
            {
                Debug.Log($"Card {_cardData?.CardName} was destroyed!");
            }
        }

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
            _memoryPool = memoryPool;
            gameObject.SetActive(true);
            InitializeCard();
        }

        public void ReturnToPool()
        {
            _memoryPool?.Despawn(this);
        }

        private void InitializeCard()
        {
            if (_cardData == null)
                return;

            _currentHealth = _cardData.CardAttackData.DefensePoint;
            _cardVisualHandler.InitializeVisuals(_cardData);
        }

        public void DropToDeckSlot()
        {
            transform.position = _cardDeckCollisionHandler.CollidedDeckSpot.transform.position;
        }
    }
}