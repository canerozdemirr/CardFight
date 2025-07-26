using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.CardPlayers.Data
{
    [Serializable]
    public class CardPlayerData
    {
        [SerializeField] private PlayerOccupation _playerOccupation;
        public PlayerOccupation PlayerOccupation => _playerOccupation;
    }

    public enum PlayerOccupation
    {
        Player,
        Bot
    }
}
