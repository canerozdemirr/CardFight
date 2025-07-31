using System;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Gameplay.Cards;
using UnityEngine;

namespace _Game.Scripts.Skills
{
    [Serializable]
    public class AttackBoostSkill : BaseSkill
    {
        [SerializeField] private int attackBoost = 10;
        
        public override string SkillName => "Attack Boost";
        public override string Description => $"Increases next played card attack by {attackBoost} points";

        public AttackBoostSkill()
        {
        }

        public override void Apply()
        {
            if (isApplied || targetPlayer == null) 
                return;
                
            // Subscribe to card played event to apply boost
            targetPlayer.OnCardPlayed += ApplyAttackBoost;
            isApplied = true;
            Debug.Log($"Applied {SkillName} to player: next card gets +{attackBoost} attack");
        }

        public override void Remove()
        {
            if (!isApplied || targetPlayer == null) 
                return;
                
            targetPlayer.OnCardPlayed -= ApplyAttackBoost;
            isApplied = false;
            Debug.Log($"Removed {SkillName} from player");
        }

        private void ApplyAttackBoost(Card card)
        {
            if (card != null)
            {
                // For now, this is a conceptual boost - in a full implementation,
                // the Card class would need a modifier system
                Debug.Log($"Attack boost applied to {card.CardData?.CardName}: +{attackBoost} attack");
                
                // Auto-remove after one use
                Remove();
            }
        }
    }
}