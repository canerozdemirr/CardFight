namespace _Game.Scripts.Commands.UICommands
{
    using System;
    using Cysharp.Threading.Tasks;
    using Interfaces.Commands;
    using Interfaces.UI;
    using UI;
    using Zenject;

    [Serializable]
    public class OpenCombatUICommand : ICommand
    {
        [Inject]
        private IUIManagementSystem _uiManagementSystem;
        
        public UniTask Execute()
        {
            _uiManagementSystem.OpenUI<CombatCanvas>();
            return UniTask.CompletedTask;
        }
    }
}