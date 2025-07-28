using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Systems;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.InputCommands
{
    [Serializable]
    public class DisableInputCommand : ICommand
    {
        [Inject] 
        private ICardInputSystem _cardInputSystem;
        public UniTask Execute()
        {
            _cardInputSystem.CloseInput();
            return UniTask.CompletedTask;
        }
    }
}