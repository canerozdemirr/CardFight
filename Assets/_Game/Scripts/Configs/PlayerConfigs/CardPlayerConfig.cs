using _Game.Scripts.Gameplay.CardPlayers.Data;
using UnityEngine;
using Zenject.SpaceFighter;

namespace _Game.Scripts.Configs.PlayerConfigs
{
    [CreateAssetMenu(fileName = "CardPlayerConfig", menuName = "Configs/Card Player Configs/New Card Player Config")]
    public class CardPlayerConfig : ScriptableObject
    {
        [SerializeField] private CardPlayerData _cardPlayerData;
        public CardPlayerData CardPlayerData => _cardPlayerData;
        
        [SerializeField] private CardPlayerHealthData _cardPlayerHealthData;
        public CardPlayerHealthData CardPlayerHealthData => _cardPlayerHealthData;

        [SerializeField] private PlayerTurnData _playerTurnData;
        public PlayerTurnData PlayerTurnData => _playerTurnData;
    }
}
