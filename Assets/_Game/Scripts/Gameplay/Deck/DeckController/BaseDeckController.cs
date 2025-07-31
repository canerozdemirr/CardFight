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
    using Cysharp.Threading.Tasks;
    using DeckSpots;
    using Events.Deck;
    using GenericEventBus;
    using Health;
    using Interfaces.Events;
    using Interfaces.Health;
    using Interfaces.Players;
    using Interfaces.Systems;

    public abstract class BaseDeckController : MonoBehaviour, ICardPlayer, IPlayerDeck
    {
        [BoxGroup("Deck Settings")]
        [SerializeField] 
        protected PlayerOccupation _playerOccupation;
        
        [BoxGroup("Deck Settings")]
        [SerializeField] 
        protected DeckSpot[] _deckPoints;
        
        [BoxGroup("Deck Settings")]
        [SerializeField] 
        protected DeckSpot _playingDeckSpot;
        
        protected List<Card> _cardList;

        [Inject] 
        protected CardPlayerListConfig _cardPlayerListConfig;

        [Inject] 
        protected IDeckBuildingSystem _deckBuildingSystem;
        
        [Inject] 
        protected ICombatRegister _combatRegister;
        
        [Inject] 
        protected ICombatSystem _combatSystem;
        
        [Inject]
        protected GenericEventBus<IEvent> _eventBus;

        protected CardPlayerData _cardPlayerData;
        protected CardPlayerHealthData _cardPlayerHealthData;
        
        public IHealthComponent Health { get; private set; }
        public IReadOnlyList<Card> AllCardsInHand => _cardList;
        public PlayerOccupation PlayerOccupation => _playerOccupation;
        
        public bool IsDeckSelected => _cardList.Count >= _cardPlayerData.TotalCardCount;
        
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
            }
            _cardList = new List<Card>(_cardPlayerData.TotalCardCount);
            Health = new HealthComponent(_cardPlayerHealthData.PlayerHealth);
            _deckBuildingSystem.AddPlayerDeck(this);
            _combatRegister.RegisterPlayer(this);
        }
        
        public void TakeDamage(int damage)
        {
            Health.TakeDamage(damage);
        }

        public virtual async UniTask PlayCard()
        {
            await UniTask.WaitForEndOfFrame();
        }
        
        public virtual void PrepareDeck()
        {
            Initialize();
        }

        public virtual void AddCardToDeck(Card card)
        {
            if (!_cardList.Contains(card))
                _cardList.Add(card);

            _eventBus.Raise(new OnPlayerCardAddedToPlayDeck(this));
        }

        public virtual void RemoveCardFromDeck(Card card)
        {
            if (_cardList.Contains(card))
                _cardList.Remove(card);
            
            _eventBus.Raise(new OnPlayerCardRemovedFromPlayerDeck(this));
        }

        public virtual async UniTask ArrangeDeck()
        {
            await UniTask.WaitForEndOfFrame();
        }
    }
}

