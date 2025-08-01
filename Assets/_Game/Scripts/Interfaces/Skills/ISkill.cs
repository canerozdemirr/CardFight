using _Game.Scripts.Interfaces.Players;
using Zenject;

namespace _Game.Scripts.Interfaces.Skills
{
    public enum SkillTargetType
    {
        Owner,
        Opponent
    }

    public interface ISkill
    {
        string SkillName { get; }
        string Description { get; }
        SkillTargetType TargetType { get; }
        void Initialize(ICardPlayer target);
        void Apply();
        void Remove();
        void IncreaseTurnNumber();
        bool IsSkillLifeTimeOver();
    }
}