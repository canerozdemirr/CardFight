using System;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Gameplay.Cards;
using UnityEngine;

namespace _Game.Scripts.Skills
{
    [Serializable]
    public class DefenseBoostSkill : BaseSkill
    {
        [SerializeField] private int defenseBoost = 8;
        
        public override string SkillName => "Defense Boost";
        public override string Description => $"Increases next played card defense by {defenseBoost} points";
        public override SkillTargetType TargetType => SkillTargetType.Owner;

        public DefenseBoostSkill()
        {
        }

        public override void Apply()
        {
            if (isApplied || targetPlayer == null) 
                return;
                
            // Subscribe to card played event to apply boost
            targetPlayer.OnCardPlayed += ApplyDefenseBoost;
            isApplied = true;
            Debug.Log($"Applied {SkillName} to player: next card gets +{defenseBoost} defense");
        }

        public override void Remove()
        {
            if (!isApplied || targetPlayer == null) 
                return;
                
            targetPlayer.OnCardPlayed -= ApplyDefenseBoost;
            isApplied = false;
            Debug.Log($"Removed {SkillName} from player");
        }

        private void ApplyDefenseBoost(Card card)
        {
            if (card != null)
            {
                // For now, this is a conceptual boost - in a full implementation,
                // the Card class would need a modifier system
                Debug.Log($"Defense boost applied to {card.CardData?.CardName}: +{defenseBoost} defense");
                
                // Auto-remove after one use
                Remove();
            }
        }
    }
}