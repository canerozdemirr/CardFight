using UnityEngine;

namespace _Game.Scripts.Gameplay.CardPlayers.Data
{
    [System.Serializable]
    public struct PlayerTurnData
    {
        [Range(5, 30)]
        public int turnDurationInSeconds;
    }
}