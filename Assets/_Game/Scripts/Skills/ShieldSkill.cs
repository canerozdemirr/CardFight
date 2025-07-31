using System;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Players;
using UnityEngine;

namespace _Game.Scripts.Skills
{
    [Serializable]
    public class ShieldSkill : BaseSkill
    {
        [SerializeField] private int shieldAmount = 15;
        
        [NonSerialized]
        private int originalMaxHealth;
        
        public override string SkillName => "Shield";
        public override string Description => $"Provides {shieldAmount} shield points to the player";
        public override SkillTargetType TargetType => SkillTargetType.Owner;

        public ShieldSkill()
        {
        }

        public override void Apply()
        {
            if (isApplied || targetPlayer?.Health == null) 
                return;
                
            originalMaxHealth = targetPlayer.Health.MaxHealth;
            targetPlayer.Health.SetMaxHealth(originalMaxHealth + shieldAmount);
            targetPlayer.Health.Heal(shieldAmount); // Restore the shield amount as health
            isApplied = true;
            Debug.Log($"Applied {SkillName} to player: +{shieldAmount} shield");
        }

        public override void Remove()
        {
            if (!isApplied || targetPlayer?.Health == null) 
                return;
                
            // Reduce max health back to original, ensuring current health doesn't exceed it
            targetPlayer.Health.SetMaxHealth(originalMaxHealth);
            isApplied = false;
            Debug.Log($"Removed {SkillName} from player: -{shieldAmount} shield");
        }
    }
}