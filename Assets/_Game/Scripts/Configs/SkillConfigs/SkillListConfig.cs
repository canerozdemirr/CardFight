using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Configs.SkillConfigs
{
    [CreateAssetMenu(fileName = "Skills Config", menuName = "CardFight/Skills Configuration")]
    public class SkillListConfig : ScriptableObject
    {
        [SerializeField] private List<SkillConfig> availableSkills = new();
        public IReadOnlyList<SkillConfig> AvailableSkills => availableSkills;
    }
}