using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Configs.Skills;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public class SkillSystem : ISkillSystem, IInitializable, IDisposable
    {
        private readonly List<ISkill> _activeSkills = new();
        private readonly List<SkillConfig> _availableSkillConfigs = new();
        
        [Inject]
        private ICombatRegister _combatRegister;
        
        [Inject(Optional = true)]
        private SkillsConfig _skillsConfig;

        public IReadOnlyList<ISkill> ActiveSkills => _activeSkills;

        public void Initialize()
        {
            PopulateAvailableSkillConfigs();
            Debug.Log($"Skill System initialized with {_availableSkillConfigs.Count} available skill configurations");
        }

        public void Dispose()
        {
            // Remove all active skills
            var skillsToRemove = _activeSkills.ToList();
            foreach (var skill in skillsToRemove)
            {
                RemoveSkill(skill);
            }
            
            _activeSkills.Clear();
            _availableSkillConfigs.Clear();
        }

        public void AddSkill(ISkill skill)
        {
            if (skill == null || _activeSkills.Contains(skill))
                return;
                
            _activeSkills.Add(skill);
            skill.Apply();
            Debug.Log($"Added skill: {skill.SkillName} (Total active: {_activeSkills.Count})");
        }

        public void RemoveSkill(ISkill skill)
        {
            if (skill == null || !_activeSkills.Contains(skill))
                return;
                
            skill.Remove();
            _activeSkills.Remove(skill);
            Debug.Log($"Removed skill: {skill.SkillName} (Total active: {_activeSkills.Count})");
        }

        public ISkill PickRandomSkill()
        {
            if (_availableSkillConfigs.Count == 0)
            {
                Debug.LogWarning("No available skill configurations to pick from");
                return null;
            }

            // Get players to apply skills to
            var players = _combatRegister.RegisteredPlayers;
            if (players.Count == 0) 
            {
                Debug.LogWarning("No registered players found for skill system");
                return null;
            }

            // Pick a random skill config
            int randomConfigIndex = UnityEngine.Random.Range(0, _availableSkillConfigs.Count);
            var selectedSkillConfig = _availableSkillConfigs[randomConfigIndex];
            
            // Pick a random player
            int randomPlayerIndex = UnityEngine.Random.Range(0, players.Count);
            var selectedPlayer = players[randomPlayerIndex];

            // Create a copy of the skill and initialize it for the selected player
            var skillImplementation = selectedSkillConfig.SkillImplementation;
            if (skillImplementation == null)
            {
                Debug.LogWarning("Selected skill config has null skill implementation");
                return null;
            }

            // Create a copy of the skill (since we can't modify the original ScriptableObject reference)
            var skillCopy = (ISkill)Activator.CreateInstance(skillImplementation.GetType());
            skillCopy.Initialize(selectedPlayer);
            
            Debug.Log($"Picked random skill: {skillCopy.SkillName} for player: {selectedPlayer.name}");
            return skillCopy;
        }

        public void ApplyRandomSkill()
        {
            var randomSkill = PickRandomSkill();
            if (randomSkill != null)
            {
                AddSkill(randomSkill);
            }
        }

        private void PopulateAvailableSkillConfigs()
        {
            _availableSkillConfigs.Clear();
            
            if (_skillsConfig?.AvailableSkills != null)
            {
                _availableSkillConfigs.AddRange(_skillsConfig.AvailableSkills);
                Debug.Log($"Populated {_availableSkillConfigs.Count} skill configurations from SkillsConfig");
            }
            else
            {
                Debug.LogWarning("No SkillsConfig injected - skill system will have no available skills");
            }
        }
    }
}