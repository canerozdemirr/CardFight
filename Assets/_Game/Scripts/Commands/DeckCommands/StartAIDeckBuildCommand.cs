using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Deck;
using _Game.Scripts.Interfaces.Players;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.DeckCommands
{
    [Serializable]
    public class StartAIDeckBuildCommand : ICommand
    {
        [Inject] 
        private IAIDeckBuilder _aiDeckBuilder;
        public UniTask Execute()
        {
            _aiDeckBuilder.PrepareAIDeck();
            return UniTask.CompletedTask;
        }
    }
}
