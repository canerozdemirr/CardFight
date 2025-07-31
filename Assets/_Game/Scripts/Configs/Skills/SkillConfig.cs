using System;
using UnityEngine;

namespace _Game.Scripts.Configs.Skills
{
    [CreateAssetMenu(fileName = "Skill Config", menuName = "CardFight/Skill Configuration")]
    [Serializable]
    public class SkillConfig : ScriptableObject
    {
        [Header("Skill Values")]
        [SerializeField, Range(5, 50)] private int healthBoostAmount = 20;
        [SerializeField, Range(5, 30)] private int shieldAmount = 15;
        [SerializeField, Range(3, 20)] private int attackBoostAmount = 10;
        [SerializeField, Range(3, 20)] private int defenseBoostAmount = 8;
        
        [Header("Skill Application Settings")]
        [SerializeField, Range(0f, 1f)] private float skillApplicationChance = 0.3f;
        [SerializeField] private bool allowMultipleSkillsPerPlayer = true;
        [SerializeField] private int maxSkillsPerPlayer = 3;

        public int HealthBoostAmount => healthBoostAmount;
        public int ShieldAmount => shieldAmount;
        public int AttackBoostAmount => attackBoostAmount;
        public int DefenseBoostAmount => defenseBoostAmount;
        public float SkillApplicationChance => skillApplicationChance;
        public bool AllowMultipleSkillsPerPlayer => allowMultipleSkillsPerPlayer;
        public int MaxSkillsPerPlayer => maxSkillsPerPlayer;
    }
}