using System;
using System.Collections.Generic;
using _Game.Scripts.Events.Skill;
using _Game.Scripts.Gameplay.Skills.Data;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Systems;
using GenericEventBus;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public class SkillSystem : ISkillSystem, IInitializable, IDisposable
    {
        private List<SkillData> _availableSkills = new();
        
        [Inject] private GenericEventBus<IEvent> _eventBus;
        
        public event Action<SkillData> OnSkillActivated;
        
        public IReadOnlyList<SkillData> AvailableSkills => _availableSkills;
        
        public void Initialize()
        {
            InitializeSkills();
            _eventBus.SubscribeTo<OnSkillUsed>(HandleSkillUsed);
            Debug.Log("Skill System initialized");
        }
        
        public void Dispose()
        {
            _eventBus.UnsubscribeFrom<OnSkillUsed>(HandleSkillUsed);
            _availableSkills.Clear();
            OnSkillActivated = null;
        }
        
        private void InitializeSkills()
        {
            _availableSkills.Add(new SkillData("Power Strike", "Increases next attack by 5 points", SkillType.AttackBoost, 5));
            _availableSkills.Add(new SkillData("Shield Wall", "Increases defense by 3 points for this turn", SkillType.DefenseBoost, 3));
            _availableSkills.Add(new SkillData("Quick Heal", "Restore 4 health points", SkillType.Heal, 4));
            _availableSkills.Add(new SkillData("Lightning Bolt", "Deal 3 direct damage to opponent", SkillType.DirectDamage, 3));
            _availableSkills.Add(new SkillData("Card Draw", "Draw an additional card", SkillType.DrawCard, 1));
        }
        
        public SkillData PickRandomSkill()
        {
            if (_availableSkills == null || _availableSkills.Count == 0)
            {
                Debug.LogWarning("No skills available to pick from");
                return null;
            }
            
            int randomIndex = UnityEngine.Random.Range(0, _availableSkills.Count);
            return _availableSkills[randomIndex];
        }
        
        public void ActivateSkill(SkillData skill)
        {
            if (skill == null)
            {
                Debug.LogWarning("Cannot activate null skill");
                return;
            }
            
            if (!CanUseSkill(skill))
            {
                Debug.LogWarning($"Cannot use skill: {skill.SkillName}");
                return;
            }
            
            Debug.Log($"Activating skill: {skill.SkillName} - {skill.Description}");
            ProcessSkillEffect(skill);
            OnSkillActivated?.Invoke(skill);
        }
        
        public bool CanUseSkill(SkillData skill)
        {
            return skill != null && _availableSkills.Contains(skill);
        }
        
        private void HandleSkillUsed(ref OnSkillUsed eventData)
        {
            ActivateSkill(eventData.UsedSkill);
        }
        
        private void ProcessSkillEffect(SkillData skill)
        {
            switch (skill.SkillType)
            {
                case SkillType.AttackBoost:
                    Debug.Log($"Attack boosted by {skill.Value} points!");
                    // TODO: Implement attack boost logic
                    break;
                case SkillType.DefenseBoost:
                    Debug.Log($"Defense boosted by {skill.Value} points!");
                    // TODO: Implement defense boost logic
                    break;
                case SkillType.Heal:
                    Debug.Log($"Healed for {skill.Value} health points!");
                    // TODO: Implement healing logic
                    break;
                case SkillType.DirectDamage:
                    Debug.Log($"Dealt {skill.Value} direct damage!");
                    // TODO: Implement direct damage logic
                    break;
                case SkillType.DrawCard:
                    Debug.Log($"Drawing {skill.Value} additional card(s)!");
                    // TODO: Implement card draw logic
                    break;
                default:
                    Debug.LogWarning($"Unknown skill type: {skill.SkillType}");
                    break;
            }
        }
    }
}