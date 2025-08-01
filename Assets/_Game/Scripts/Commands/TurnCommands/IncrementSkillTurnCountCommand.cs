using System;
using _Game.Scripts.Interfaces.Systems;
using Cysharp.Threading.Tasks;
using Zenject;
using ICommand = _Game.Scripts.Interfaces.Commands.ICommand;

namespace _Game.Scripts.Commands.TurnCommands
{
    [Serializable]
    public class IncrementSkillTurnCountCommand : ICommand
    {
        [Inject]
        private ISkillSystem _skillSystem;
        
        public UniTask Execute()
        {
            _skillSystem.OnTurnEnd();
            return UniTask.CompletedTask;
        }
    }
}