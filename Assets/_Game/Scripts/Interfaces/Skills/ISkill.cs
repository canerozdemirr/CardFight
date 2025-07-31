using _Game.Scripts.Interfaces.Players;

namespace _Game.Scripts.Interfaces.Skills
{
    public enum SkillTargetType
    {
        Owner,      // Targets the player who owns/triggered the skill
        Opponent    // Targets the opponent(s) of the skill owner
    }

    public interface ISkill
    {
        string SkillName { get; }
        string Description { get; }
        SkillTargetType TargetType { get; }
        void Initialize(ICardPlayer target);
        void Apply();
        void Remove();
    }
}