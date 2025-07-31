using _Game.Scripts.Interfaces.Skills;
using UnityEngine;

namespace _Game.Scripts.Configs.SkillConfigs
{
    [CreateAssetMenu(fileName = "SkillConfig", menuName = "Game/Configs/Skill Config")]
    public class SkillConfig : ScriptableObject
    {
        [SerializeReference, SubclassSelector]
        [SerializeField] private ISkill skillImplementation;

        public ISkill SkillImplementation => skillImplementation;
    }
}