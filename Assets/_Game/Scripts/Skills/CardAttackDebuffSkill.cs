using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Systems;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Game.Scripts.Skills
{
    [System.Serializable]
    public class CardAttackDebuffSkill : BaseSkill
    {
        [FormerlySerializedAs("attackDebuff")] 
        [SerializeField] private int attackDebuff = 5;
        
        public override string SkillName => "Attack Debuff";
        public override string Description => $"Decreases opponent's cards attack by {attackDebuff} points";
        public override SkillTargetType TargetType => SkillTargetType.Opponent;
        
        [Inject] private ICombatSystem _combatSystem;

        public override void Apply()
        {
            if (isApplied || targetPlayer == null) 
                return;

            Card playedCard = _combatSystem.GetCardInCombat(targetPlayer);
            playedCard.AddAttackPoint(-attackDebuff);
            isApplied = true;
        }

        public override void Remove()
        {
            if (!isApplied || targetPlayer == null) 
                return;
            
            isApplied = false;
        }
    }
}
