using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Skills.Data
{
    [Serializable]
    public class SkillData
    {
        [SerializeField] private string _skillName;
        [SerializeField] private string _description;
        [SerializeField] private SkillType _skillType;
        [SerializeField] private int _value;
        
        public string SkillName => _skillName;
        public string Description => _description;
        public SkillType SkillType => _skillType;
        public int Value => _value;

        public SkillData(string skillName, string description, SkillType skillType, int value)
        {
            _skillName = skillName;
            _description = description;
            _skillType = skillType;
            _value = value;
        }
    }
    
    public enum SkillType
    {
        AttackBoost,
        DefenseBoost,
        Heal,
        DirectDamage,
        DrawCard
    }
}