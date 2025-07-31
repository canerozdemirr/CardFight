using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Cards.Data
{
    [Serializable]
    public struct CardAttackData
    {
        [SerializeField, Range(0, 100)] private int _attackPoint;
        [SerializeField, Range(0, 100)] private int _defensePoint;
        [SerializeField, Range(1, 100)] private int _health;

        public int AttackPoint => _attackPoint;
        public int DefensePoint => _defensePoint;
        public int Health => _health;
    }
}
