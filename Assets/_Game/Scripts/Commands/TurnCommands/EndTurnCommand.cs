using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Systems;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.TurnCommands
{
    [Serializable]
    public class EndTurnCommand : ICommand
    {
        [Inject]
        private ITurnSystem _turnSystem;
        
        public UniTask Execute()
        {
            _turnSystem.EndTurn();
            return UniTask.CompletedTask;
        }
    }
}