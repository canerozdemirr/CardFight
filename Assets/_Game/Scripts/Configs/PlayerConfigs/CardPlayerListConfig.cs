using UnityEngine;

namespace _Game.Scripts.Configs.PlayerConfigs
{
    [CreateAssetMenu(fileName = "Card Player List Config", menuName = "Configs/Card Player Configs/New Card List Player Config")]
    public class CardPlayerListConfig : ScriptableObject
    {
        [SerializeField] private CardPlayerConfig[] _cardPlayerConfigs;
        public CardPlayerConfig[] CardPlayerConfigs => _cardPlayerConfigs;
    }
}
