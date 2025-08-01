using System;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Players;
using UnityEngine;

namespace _Game.Scripts.Skills
{
    [Serializable]
    public class PlayerHealthBoostSkill : BaseSkill
    {
        [SerializeField] private int _healthBoost = 20;
        
        public override string SkillName => "Health Boost";
        public override string Description => $"Increases player health by {_healthBoost} points";
        public override SkillTargetType TargetType => SkillTargetType.Owner;

        public PlayerHealthBoostSkill()
        {
        }

        public override void Apply()
        {
            if (isApplied || targetPlayer?.Health == null) 
                return;
                
            targetPlayer.Health.Heal(_healthBoost);
            isApplied = true;
            Debug.Log($"Applied {SkillName} to player: +{_healthBoost} health");
        }

        public override void Remove()
        {
            if (!isApplied || targetPlayer?.Health == null) 
                return;
            
            targetPlayer.Health.TakeDamage(_healthBoost);
            isApplied = false;
            Debug.Log($"Removed {SkillName} from player (no effect - permanent boost)");
        }
    }
}