using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Skills;
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

        public IReadOnlyList<ISkill> ActiveSkills => _activeSkills;

        public void Initialize()
        {
            PopulateAvailableSkills();
            Debug.Log("Skill System initialized");
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
            Debug.Log($"Added skill: {skill.SkillName}");
        }

        public void RemoveSkill(ISkill skill)
        {
            if (skill == null || !_activeSkills.Contains(skill))
                return;
                
            skill.Remove();
            _activeSkills.Remove(skill);
            Debug.Log($"Removed skill: {skill.SkillName}");
        }

        public ISkill PickRandomSkill()
        {
            if (_availableSkills.Count == 0)
            {
                PopulateAvailableSkills();
            }

            if (_availableSkills.Count == 0)
                return null;

            int randomIndex = UnityEngine.Random.Range(0, _availableSkills.Count);
            return _availableSkills[randomIndex];
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
            
            // Get random players to apply skills to
            var players = _combatRegister.RegisteredPlayers;
            if (players.Count == 0) return;

            foreach (var player in players)
            {
                // Create one of each skill type for each player
                _availableSkills.Add(new HealthBoostSkill(player));
                _availableSkills.Add(new ShieldSkill(player));
                _availableSkills.Add(new AttackBoostSkill(player));
                _availableSkills.Add(new DefenseBoostSkill(player));
            }
        }
    }
}