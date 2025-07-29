using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.UI;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.UICommands
{
    [Serializable]
    public class OpenTurnUICommand : ICommand
    {
        [Inject]
        private IUIManagementSystem _uiManagementSystem;
        
        public UniTask Execute()
        {
            _uiManagementSystem.OpenUI<TurnCanvas>();
            return UniTask.CompletedTask;
        }
    }
}