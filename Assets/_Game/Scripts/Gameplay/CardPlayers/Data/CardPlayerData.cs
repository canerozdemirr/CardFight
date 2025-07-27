using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.CardPlayers.Data
{
    [Serializable]
    public struct CardPlayerData
    {
        [SerializeField] private PlayerOccupation _playerOccupation;
        public PlayerOccupation PlayerOccupation => _playerOccupation;
        
        [SerializeField] private int _totalCardCount;
        public int TotalCardCount => _totalCardCount;
    }

    public enum PlayerOccupation
    {
        Player = 0,
        Bot = 1
    }
}
