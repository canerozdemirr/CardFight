using System;
using UnityEngine;

namespace _Game.Scripts.Configs.TurnConfigs
{
    [Serializable]
    public struct TurnConfigData
    {
        [SerializeField, Range(1, 30)] private int _turnDurationInSeconds;
        [SerializeField, Range(1, 6)] private int _maxTurnsBeforeGameEnd;
        
        public int TurnDurationInSeconds => _turnDurationInSeconds;
        public int MaxTurnsBeforeGameEnd => _maxTurnsBeforeGameEnd;

        public TurnConfigData(int turnDurationInSeconds, int maxTurnsBeforeGameEnd)
        {
            _turnDurationInSeconds = turnDurationInSeconds;
            _maxTurnsBeforeGameEnd = maxTurnsBeforeGameEnd;
        }
    }
}
