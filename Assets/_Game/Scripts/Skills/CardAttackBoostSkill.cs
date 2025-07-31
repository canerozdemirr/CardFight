using System;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Gameplay.Cards;
using UnityEngine;

namespace _Game.Scripts.Skills
{
    [Serializable]
    public class CardAttackBoostSkill : BaseSkill
    {
        [SerializeField] private int attackBoost = 10;
        
        public override string SkillName => "Attack Boost";
        public override string Description => $"Increases next played card attack by {attackBoost} points";
        public override SkillTargetType TargetType => SkillTargetType.Owner;

        private int _cardPointDifference = 0;

        public CardAttackBoostSkill()
        {
        }

        public override void Apply()
        {
            if (isApplied || targetPlayer == null) 
                return;

            foreach (Card card in targetPlayer.AllCardsInHand)
            {
                card.AddAttackPoint(_cardPointDifference);
            }
            
            isApplied = true;
            Debug.Log($"Applied {SkillName} to player: next card gets +{attackBoost} attack");
        }

        public override void Remove()
        {
            if (!isApplied || targetPlayer == null) 
                return;
            
            foreach (Card card in targetPlayer.AllCardsInHand)
            {
                card.AddAttackPoint(-_cardPointDifference);
            }
            
            isApplied = false;
            Debug.Log($"Removed {SkillName} from player");
        }
    }
}