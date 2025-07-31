using System.Collections.Generic;
using _Game.Scripts.Configs.Skills;
using UnityEngine;

namespace _Game.Scripts.Configs.SkillConfigs
{
    [CreateAssetMenu(fileName = "Skills Config", menuName = "CardFight/Skills Configuration")]
    public class SkillListConfig : ScriptableObject
    {
        [Header("Available Skills")]
        [SerializeField] private List<SkillConfig> availableSkills = new();

        public IReadOnlyList<SkillConfig> AvailableSkills => availableSkills;
    }
}