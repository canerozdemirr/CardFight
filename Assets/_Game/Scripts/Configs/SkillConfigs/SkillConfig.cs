using System;
using _Game.Scripts.Interfaces.Skills;
using MackySoft.SerializeReferenceExtensions;
using UnityEngine;

namespace _Game.Scripts.Configs.Skills
{
    [Serializable]
    public class SkillConfig
    {
        [SerializeReference, SubclassSelector]
        [SerializeField] private ISkill skillImplementation;

        public ISkill SkillImplementation => skillImplementation;
    }
}