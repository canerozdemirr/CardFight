using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Skills;

namespace _Game.Scripts.Events.Skill
{
    public struct OnSkillApplied : IEvent
    {
        public ISkill AppliedSkill { get; }

        public OnSkillApplied(ISkill appliedSkill)
        {
            AppliedSkill = appliedSkill;
        }
    }
}