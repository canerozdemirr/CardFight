using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Configs.SkillConfigs;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Events.Skill;
using _Game.Scripts.Interfaces.Events;
using GenericEventBus;
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
        
        [Inject]
        private SkillListConfig _skillListConfig;
        
        [Inject]
        private GenericEventBus<IEvent> _eventBus;

        public IReadOnlyList<ISkill> ActiveSkills => _activeSkills;

        private ISkill _pickedSkill;

        public void Initialize()
        {
            PopulateAvailableSkillConfigs();
            _eventBus.SubscribeTo<OnSkillUsed>(OnSkillUsed);
            Debug.Log($"Skill System initialized with {_availableSkillConfigs.Count} available skill configurations");
        }

        public void Dispose()
        {
            List<ISkill> skillsToRemove = _activeSkills.ToList();
            foreach (ISkill skill in skillsToRemove)
            {
                RemoveSkill(skill);
            }
            
            _activeSkills.Clear();
            _availableSkillConfigs.Clear();
            _eventBus.UnsubscribeFrom<OnSkillUsed>(OnSkillUsed);
        }

        public void AddSkill(ISkill skill)
        {
            if (skill == null || _activeSkills.Contains(skill))
                return;
                
            _activeSkills.Add(skill);
            
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

        public ISkill PickRandomSkill(ICardPlayer skillOwner)
        {
            if (_availableSkillConfigs.Count == 0)
            {
                Debug.LogWarning("No available skill configurations to pick from");
                return null;
            }

            if (skillOwner == null)
            {
                Debug.LogWarning("Skill owner is null, cannot pick random skill");
                return null;
            }
            
            IReadOnlyList<ICardPlayer> players = _combatRegister.RegisteredPlayers;
            if (players.Count == 0) 
            {
                Debug.LogWarning("No registered players found for skill system");
                return null;
            }
            
            int randomConfigIndex = UnityEngine.Random.Range(0, _availableSkillConfigs.Count);
            SkillConfig selectedSkillConfig = _availableSkillConfigs[randomConfigIndex];
            
            ISkill skillImplementation = selectedSkillConfig.SkillImplementation;
            if (skillImplementation == null)
            {
                Debug.LogWarning("Selected skill config has null skill implementation");
                return null;
            }

            ICardPlayer targetPlayer = DetermineSkillTarget(skillImplementation.TargetType, skillOwner, players);
            if (targetPlayer == null)
            {
                Debug.LogWarning($"Could not determine target for skill {skillImplementation.SkillName} with target type {skillImplementation.TargetType}");
                return null;
            }
            
            ISkill skillCopy = (ISkill)Activator.CreateInstance(skillImplementation.GetType());
            skillCopy.Initialize(targetPlayer);
            
            Debug.Log($"Picked random skill: {skillCopy.SkillName} for owner: {skillOwner}, targeting: {targetPlayer}");
            return skillCopy;
        }

        public void ApplyRandomSkill(ICardPlayer skillOwner)
        {
            ISkill randomSkill = PickRandomSkill(skillOwner);
            _pickedSkill = randomSkill;
            if (randomSkill != null)
            {
                AddSkill(randomSkill);
            }
        }

        private ICardPlayer DetermineSkillTarget(SkillTargetType targetType, ICardPlayer skillOwner, IReadOnlyList<ICardPlayer> allPlayers)
        {
            switch (targetType)
            {
                case SkillTargetType.Owner:
                    return skillOwner;
                
                case SkillTargetType.Opponent:
                    
                    List<ICardPlayer> opponents = allPlayers.Where(p => p != skillOwner && p.PlayerOccupation != skillOwner.PlayerOccupation).ToList();
                    if (opponents.Count == 0)
                    {
                        Debug.LogWarning("No opponents found for skill targeting");
                        return null;
                    }
                  
                    int randomOpponentIndex = UnityEngine.Random.Range(0, opponents.Count);
                    return opponents[randomOpponentIndex];
                
                default:
                    Debug.LogWarning($"Unknown skill target type: {targetType}");
                    return null;
            }
        }

        private void PopulateAvailableSkillConfigs()
        {
            _availableSkillConfigs.Clear();
            
            if (_skillListConfig.AvailableSkills != null)
            {
                _availableSkillConfigs.AddRange(_skillListConfig.AvailableSkills);
                Debug.Log($"Populated {_availableSkillConfigs.Count} skill configurations from SkillsConfig");
            }
            else
            {
                Debug.LogWarning("No SkillsConfig injected - skill system will have no available skills");
            }
        }
        
        private void OnSkillUsed(ref OnSkillUsed eventData)
        {
            _pickedSkill?.Apply();
        }
    }
}