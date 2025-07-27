using System.Collections.Generic;
using _Game.Scripts.Configs.PlayerConfigs;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck.DeckController
{
    public abstract class BaseDeckController : MonoBehaviour
    {
        [BoxGroup("Deck Settings")]
        [SerializeField] 
        protected PlayerOccupation _playerOccupation;
        
        [BoxGroup("Deck Settings")]
        [SerializeField] 
        protected Transform[] _deckPoints;
        
        protected List<Cards.Card> _cardList;

        [Inject] 
        protected CardPlayerListConfig _cardPlayerListConfig;

        protected int _totalCardCount;
        
        public bool IsDeckCompleted => _cardList.Count >= _totalCardCount;
        
        public virtual void Initialize()
        {
            foreach (CardPlayerConfig cardPlayerConfig in _cardPlayerListConfig.CardPlayerConfigs)
            {
                if (cardPlayerConfig.CardPlayerData.PlayerOccupation != _playerOccupation) 
                    continue;
                
                _totalCardCount = cardPlayerConfig.CardPlayerData.TotalCardCount;
                break;
            }
            _cardList = new List<Cards.Card>(_totalCardCount);
        }

        public virtual void AddCard(Cards.Card card)
        {
            if (!_cardList.Contains(card))
            {
                _cardList.Add(card);
            }
            else
            {
                Debug.LogWarning($"Card {card.name} is already in the deck.");
            }
        }

        public virtual void RemoveCard(Cards.Card card)
        {
            if (_cardList.Contains(card))
            {
                _cardList.Remove(card);
            }
            else
            {
                Debug.LogWarning($"Card {card.name} not found in the deck.");
            }
        }
        
        public virtual void PlayCard()
        {
            
        }
    }
}
