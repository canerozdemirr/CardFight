using System;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Gameplay.Cards;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Skills
{
    [Serializable]
    public class CardAttackBoostSkill : BaseSkill
    {
        [FormerlySerializedAs("attackBoost")] 
        [SerializeField] private int _attackBoost = 10;
        
        public override string SkillName => "Attack Boost";
        public override string Description => $"Increases next played card attack by {_attackBoost} points";
        public override SkillTargetType TargetType => SkillTargetType.Owner;

        public CardAttackBoostSkill()
        {
        }

        public override void Apply()
        {
            if (isApplied || targetPlayer == null) 
                return;

            foreach (Card card in targetPlayer.AllCardsInHand)
            {
                card.AddAttackPoint(_attackBoost);
            }
            
            isApplied = true;
            Debug.Log($"Applied {SkillName} to player: next card gets +{_attackBoost} attack");
        }

        public override void Remove()
        {
            if (!isApplied || targetPlayer == null) 
                return;
            
            foreach (Card card in targetPlayer.AllCardsInHand)
            {
                card.AddAttackPoint(-_attackBoost);
            }
            
            isApplied = false;
            Debug.Log($"Removed {SkillName} from player");
        }
    }
}