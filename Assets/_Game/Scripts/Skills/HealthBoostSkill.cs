using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Players;
using UnityEngine;

namespace _Game.Scripts.Skills
{
    public class HealthBoostSkill : BaseSkill
    {
        private readonly int healthBoost;
        
        public override string SkillName => "Health Boost";
        public override string Description => $"Increases player health by {healthBoost} points";

        public HealthBoostSkill(ICardPlayer target, int boostAmount = 20) : base(target)
        {
            healthBoost = boostAmount;
        }

        public override void Apply()
        {
            if (isApplied || targetPlayer?.Health == null) 
                return;
                
            targetPlayer.Health.Heal(healthBoost);
            isApplied = true;
            Debug.Log($"Applied {SkillName} to player: +{healthBoost} health");
        }

        public override void Remove()
        {
            if (!isApplied || targetPlayer?.Health == null) 
                return;
                
            // Health boost is permanent once applied, cannot be removed
            // This represents a permanent increase rather than a temporary buff
            isApplied = false;
            Debug.Log($"Removed {SkillName} from player (no effect - permanent boost)");
        }
    }
}