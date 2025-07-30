using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Deck;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.DeckCommands
{
    public class DisablePlayerDeckCommand : ICommand
    {
        [Inject] private IPlayerDeckBuilder _playerDeckBuilder;
        public UniTask Execute()
        {
            _playerDeckBuilder.CloseDeckSlots();
            return UniTask.CompletedTask;
        }
    }
}