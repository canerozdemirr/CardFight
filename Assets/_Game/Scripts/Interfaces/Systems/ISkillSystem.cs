using System.Collections.Generic;
using _Game.Scripts.Interfaces.Skills;

namespace _Game.Scripts.Interfaces.Systems
{
    public interface ISkillSystem
    {
        IReadOnlyList<ISkill> ActiveSkills { get; }
        void AddSkill(ISkill skill);
        void RemoveSkill(ISkill skill);
        ISkill PickRandomSkill();
        void ApplyRandomSkill();
    }
}