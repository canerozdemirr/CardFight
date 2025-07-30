using System.Collections.Generic;
using _Game.Scripts.Configs.PlayerConfigs;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using NaughtyAttributes;
using UnityEngine;
using Zenject;
using System;

namespace _Game.Scripts.Gameplay.Deck.DeckController
{
    using Cards;
    using Health;
    using Interfaces.Health;
    using Interfaces.Players;

    public abstract class BaseDeckController : MonoBehaviour, ICardPlayer
    {
        [BoxGroup("Deck Settings")]
        [SerializeField] 
        protected PlayerOccupation _playerOccupation;
        
        [BoxGroup("Deck Settings")]
        [SerializeField] 
        protected Transform[] _deckPoints;
        
        protected List<Card> _cardList;

        [Inject] 
        protected CardPlayerListConfig _cardPlayerListConfig;

        protected CardPlayerData _cardPlayerData;
        protected CardPlayerHealthData _cardPlayerHealthData;
        protected PlayerTurnData _playerTurnData;
        
        public IHealthComponent Health { get; private set; }
        
        public event Action<ICardPlayer> OnPlayerDeath;
        public event Action<Card> OnCardPlayed;

        protected virtual void Initialize()
        {
            foreach (CardPlayerConfig cardPlayerConfig in _cardPlayerListConfig.CardPlayerConfigs)
            {
                if (cardPlayerConfig.CardPlayerData.PlayerOccupation != _playerOccupation) 
                    continue;
                
                _cardPlayerData = cardPlayerConfig.CardPlayerData;
                _cardPlayerHealthData = cardPlayerConfig.CardPlayerHealthData;
                _playerTurnData = cardPlayerConfig.PlayerTurnData;
            }
            _cardList = new List<Card>(_cardPlayerData.TotalCardCount);
            Health = new HealthComponent(_cardPlayerHealthData.PlayerHealth);
        }
        
        public void TakeDamage(int damage)
        {
            Health.TakeDamage(damage);
        }

        public virtual void PlayCard(Card card)
        {
            _cardList.Remove(card);
        }
    }
}

