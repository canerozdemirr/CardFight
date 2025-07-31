using System;
using _Game.Scripts.Gameplay.Cards.Data;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Card.Data
{
    [Serializable]
    public class CardData
    {
        [SerializeField] private string _cardName;
        [SerializeField] private CardAttackData _cardAttackData;
        
        public string CardName => _cardName;
        public CardAttackData CardAttackData => _cardAttackData;

        public CardData()
        {
            
        }

        public CardData(string cardName, CardAttackData cardAttackData)
        {
            _cardName = cardName;
            _cardAttackData = cardAttackData;
        }

        public CardData(CardData originalCardData)
        {
            _cardName = originalCardData.CardName;
            _cardAttackData = originalCardData.CardAttackData;
        }
    }
}