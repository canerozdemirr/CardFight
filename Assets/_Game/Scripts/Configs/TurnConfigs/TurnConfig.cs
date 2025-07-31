using UnityEngine;

namespace _Game.Scripts.Configs.TurnConfigs
{
    [CreateAssetMenu(fileName = "TurnConfig", menuName = "Game/Configs/Turn Config")]
    public class TurnConfig : ScriptableObject
    {
        [SerializeField] private TurnConfigData _turnConfigData;
        
        public TurnConfigData TurnConfigData => _turnConfigData;
        public int TurnDurationInSeconds => _turnConfigData.TurnDurationInSeconds;
        public int MaxTurnsBeforeGameEnd => _turnConfigData.MaxTurnsBeforeGameEnd;
    }
}
