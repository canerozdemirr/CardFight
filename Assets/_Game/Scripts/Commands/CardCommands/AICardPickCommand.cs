using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Players;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.CardCommands
{
    [Serializable]
    public class AICardPickCommand : ICommand
    {
        [Inject] 
        private readonly IAIDeck _aiDeck;
        public UniTask Execute()
        {
            _aiDeck.PlayCard();
            return UniTask.CompletedTask;
        }
    }
}