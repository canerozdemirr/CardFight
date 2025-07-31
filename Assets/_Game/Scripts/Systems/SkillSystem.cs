using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Skills;
using _Game.Scripts.Configs.Skills;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public class SkillSystem : ISkillSystem, IInitializable, IDisposable
    {
        private readonly List<ISkill> _activeSkills = new();
        private readonly List<ISkill> _availableSkills = new();
        
        [Inject]
        private ICombatRegister _combatRegister;
        
        [Inject(Optional = true)]
        private SkillConfig _skillConfig;

        public IReadOnlyList<ISkill> ActiveSkills => _activeSkills;

        public void Initialize()
        {
            PopulateAvailableSkills();
            Debug.Log($"Skill System initialized with {_availableSkills.Count} available skills");
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
            _availableSkills.Clear();
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
            if (_availableSkills.Count == 0)
            {
                PopulateAvailableSkills();
            }

            if (_availableSkills.Count == 0)
            {
                Debug.LogWarning("No available skills to pick from");
                return null;
            }

            int randomIndex = UnityEngine.Random.Range(0, _availableSkills.Count);
            var selectedSkill = _availableSkills[randomIndex];
            Debug.Log($"Picked random skill: {selectedSkill.SkillName}");
            return selectedSkill;
        }

        public void ApplyRandomSkill()
        {
            var randomSkill = PickRandomSkill();
            if (randomSkill != null)
            {
                AddSkill(randomSkill);
            }
        }

        private void PopulateAvailableSkills()
        {
            _availableSkills.Clear();
            
            // Get players to apply skills to
            var players = _combatRegister.RegisteredPlayers;
            if (players.Count == 0) 
            {
                Debug.LogWarning("No registered players found for skill system");
                return;
            }

            // Use configuration values if available, otherwise use defaults
            int healthBoost = _skillConfig?.HealthBoostAmount ?? 20;
            int shieldAmount = _skillConfig?.ShieldAmount ?? 15;
            int attackBoost = _skillConfig?.AttackBoostAmount ?? 10;
            int defenseBoost = _skillConfig?.DefenseBoostAmount ?? 8;

            foreach (var player in players)
            {
                // Create one of each skill type for each player
                _availableSkills.Add(new HealthBoostSkill(player, healthBoost));
                _availableSkills.Add(new ShieldSkill(player, shieldAmount));
                _availableSkills.Add(new AttackBoostSkill(player, attackBoost));
                _availableSkills.Add(new DefenseBoostSkill(player, defenseBoost));
            }
            
            Debug.Log($"Populated {_availableSkills.Count} available skills for {players.Count} players");
        }
    }
}