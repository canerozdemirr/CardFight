using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Card
{
    [Serializable]
    public class CardData
    {
        [SerializeField] private string _cardName;
        [SerializeField] private CardAttackData _cardAttackData;
        
        public string CardName => _cardName;
        public CardAttackData CardAttackData => _cardAttackData;
    }
}