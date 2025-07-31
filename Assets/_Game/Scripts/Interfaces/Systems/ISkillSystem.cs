using System;
using System.Collections.Generic;
using _Game.Scripts.Gameplay.Skills.Data;

namespace _Game.Scripts.Interfaces.Systems
{
    public interface ISkillSystem
    {
        event Action<SkillData> OnSkillActivated;
        
        IReadOnlyList<SkillData> AvailableSkills { get; }
        SkillData PickRandomSkill();
        void ActivateSkill(SkillData skill);
        bool CanUseSkill(SkillData skill);
    }
}