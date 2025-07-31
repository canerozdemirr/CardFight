using _Game.Scripts.Interfaces.Players;

namespace _Game.Scripts.Interfaces.Skills
{
    public interface ISkill
    {
        string SkillName { get; }
        string Description { get; }
        void Initialize(ICardPlayer target);
        void Apply();
        void Remove();
    }
}