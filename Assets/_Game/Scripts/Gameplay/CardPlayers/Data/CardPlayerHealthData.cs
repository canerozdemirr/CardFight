using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.CardPlayers.Data
{
    [Serializable]
    public struct CardPlayerHealthData
    {
        [SerializeField]
        private int _playerHealth;
        
        public int PlayerHealth => _playerHealth;
    }
}
