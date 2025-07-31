using _Game.Scripts.Gameplay.Skills.Data;
using _Game.Scripts.Interfaces.Events;

namespace _Game.Scripts.Events.Skill
{
    public struct OnSkillUsed : IEvent
    {
        public SkillData UsedSkill { get; }
        
        public OnSkillUsed(SkillData usedSkill)
        {
            UsedSkill = usedSkill;
        }
    }
}