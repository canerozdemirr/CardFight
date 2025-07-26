using UnityEngine;

namespace _Game.Scripts.Configs.CardConfigs
{
    [CreateAssetMenu(fileName = "Card List Config", menuName = "Configs/Card Configs/Card List Config")]
    public class CardListConfig : ScriptableObject
    {
        [SerializeField] private CardConfig[] _cardConfigList;
        public CardConfig[] CardConfigList => _cardConfigList;
    }
}
