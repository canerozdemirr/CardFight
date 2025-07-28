using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.UICommands
{
    [Serializable]
    public class OpenTurnUICommand : ICommand
    {
        [Inject]
        private TurnCanvas _turnCanvas;
        
        public UniTask Execute()
        {
            _turnCanvas.gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }
    }
}