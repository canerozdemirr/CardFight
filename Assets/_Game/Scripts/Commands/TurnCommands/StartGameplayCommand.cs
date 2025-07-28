using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Systems;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.TurnCommands
{
    [Serializable]
    public class StartGameplayCommand : ICommand
    {
        [Inject] 
        private ITurnSystem _turnSystem;
        public UniTask Execute()
        {
            _turnSystem.StartTurn();
            return UniTask.CompletedTask;
        }
    }
}