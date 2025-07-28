using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Deck;
using _Game.Scripts.Interfaces.Players;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.DeckCommands
{
    [Serializable]
    public class AwaitPlayerArrangeDeckCommand : ICommand
    {
        [Inject] private IPlayerDeck _playerDeck;
        [Inject] private IPlayerDeckBuilder _playerDeckBuilder;
        public async UniTask Execute()
        {
            _playerDeckBuilder.ClearUnusedCards();
            await _playerDeck.ArrangeDeck();
        }
    }
}