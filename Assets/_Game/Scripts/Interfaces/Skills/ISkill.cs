namespace _Game.Scripts.Interfaces.Skills
{
    public interface ISkill
    {
        string SkillName { get; }
        string Description { get; }
        void Apply();
        void Remove();
    }
}