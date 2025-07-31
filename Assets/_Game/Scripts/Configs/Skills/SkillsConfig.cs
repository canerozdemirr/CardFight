using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Configs.Skills
{
    [CreateAssetMenu(fileName = "Skills Config", menuName = "CardFight/Skills Configuration")]
    public class SkillsConfig : ScriptableObject
    {
        [Header("Available Skills")]
        [SerializeField] private List<SkillConfig> availableSkills = new();

        public IReadOnlyList<SkillConfig> AvailableSkills => availableSkills;
    }
}