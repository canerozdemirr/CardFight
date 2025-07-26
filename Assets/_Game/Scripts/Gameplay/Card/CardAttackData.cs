using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Card
{
    [Serializable]
    public struct CardAttackData
    {
        [SerializeField, Range(0, 100)] private int _attackPoint;
        [SerializeField, Range(0, 100)] private int _defensePoint;

        public int AttackPoint => _attackPoint;
        public int DefensePoint => _defensePoint;
    }
}
