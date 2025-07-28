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

        protected CardPlayerData _cardPlayerData;
        protected CardPlayerHealthData _cardPlayerHealthData;
        protected PlayerTurnData _playerTurnData;

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
            _cardList = new List<Cards.Card>(_cardPlayerData.TotalCardCount);
        }
    }
}
