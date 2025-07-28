using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Players;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.DeckCommands
{
    [Serializable]
    public class AwaitAIDeckBuildCommand : ICommand
    {
        [Inject]
        private IAIDeck _aiDeck;
        
        public async UniTask Execute()
        {
            await UniTask.WaitUntil(() => _aiDeck.IsDeckSelected);
        }
    }
}
