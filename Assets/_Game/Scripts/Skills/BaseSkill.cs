using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Players;
using UnityEngine;

namespace _Game.Scripts.Skills
{
    public abstract class BaseSkill : ISkill
    {
        public abstract string SkillName { get; }
        public abstract string Description { get; }
        
        protected ICardPlayer targetPlayer;
        protected bool isApplied = false;

        protected BaseSkill(ICardPlayer target)
        {
            targetPlayer = target;
        }

        public abstract void Apply();
        public abstract void Remove();
    }
}