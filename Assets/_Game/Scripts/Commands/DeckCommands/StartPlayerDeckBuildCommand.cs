using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Deck;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.DeckCommands
{
    [Serializable]
    public class StartPlayerDeckBuildCommand : ICommand
    {
        [Inject]
        private IPlayerDeckBuilder _playerDeckBuilder;
        public UniTask Execute()
        {
            _playerDeckBuilder.PreparePlayerDeck();
            return UniTask.CompletedTask;
        }
    }
}