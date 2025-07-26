using _Game.Scripts.Gameplay.Card;
using _Game.Scripts.Gameplay.Card.Data;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Configs.CardConfigs
{
    [CreateAssetMenu(fileName = "CardConfig", menuName = "Configs/Card Configs/New Card Config")]
    public class CardConfig : ScriptableObject
    {
        [SerializeField] private CardData _cardData;
        public CardData CardData => _cardData;
    }
}
