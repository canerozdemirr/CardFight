using System;
using _Game.Scripts.Interfaces.Skills;
using _Game.Scripts.Interfaces.Players;
using UnityEngine;

namespace _Game.Scripts.Skills
{
    [Serializable]
    public abstract class BaseSkill : ISkill
    {
        public abstract string SkillName { get; }
        public abstract string Description { get; }
        
        [NonSerialized]
        protected ICardPlayer targetPlayer;
        
        [NonSerialized]
        protected bool isApplied = false;

        protected BaseSkill()
        {
        }

        public virtual void Initialize(ICardPlayer target)
        {
            targetPlayer = target;
            isApplied = false;
        }

        public abstract void Apply();
        public abstract void Remove();
    }
}