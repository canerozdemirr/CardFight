using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Deck;
using _Game.Scripts.Interfaces.Players;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.DeckCommands
{
    using Interfaces.Systems;

    [Serializable]
    public class AwaitPlayerArrangeDeckCommand : ICommand
    {
        [Inject] 
        private IDeckBuildingSystem _deckBuildingSystem;
        
        [Inject] 
        private IPlayerDeckBuilder _playerDeckBuilder;
        
        public async UniTask Execute()
        {
            _playerDeckBuilder.ClearUnusedCards();
            _playerDeckBuilder.CloseDeckSlots();
            
            foreach (IPlayerDeck deck in _deckBuildingSystem.AllRegisteredDecks)
            {
                await deck.ArrangeDeck();
            }
        }
    }
}