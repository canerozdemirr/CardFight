using System;
using _Game.Scripts.Commands.DeckCommands;
using _Game.Scripts.Commands.InputCommands;
using _Game.Scripts.Commands.TurnCommands;
using _Game.Scripts.Gameplay.Deck.DeckBuilders;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.GameObjects;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Utilities.SubSystems;
using Zenject;

namespace _Game.Scripts.Systems
{
    using System.Collections.Generic;
    using Interfaces.Players;

    [Serializable]
    public sealed class DeckBuildingSystem : IDeckBuildingSystem, IInitializable, IDisposable
    {
        [Inject]
        private PlayerDeckBuilder _playerDeckBuilder;
        
        [Inject]
        private AIDeckBuilder _aiDeckBuilder;

        [Inject]
        private DiContainer _container;
        
        private ICommandExecutor _deckBuildingCommandExecutor;
        
        private List<IPlayerDeck> _playerDecks;
        public IReadOnlyList<IPlayerDeck> AllRegisteredDecks => _playerDecks;
        
        public void Initialize()
        {
            _playerDecks = new List<IPlayerDeck>();
            _deckBuildingCommandExecutor = new CommandExecutor(_container);
            AddCommands();
            _deckBuildingCommandExecutor.ExecuteCommands();
        }
        
        public void Dispose()
        {
            
        }

        private void AddCommands()
        {
            _deckBuildingCommandExecutor.Enqueue(new StartAIDeckBuildCommand());
            _deckBuildingCommandExecutor.Enqueue(new StartPlayerDeckBuildCommand());
            _deckBuildingCommandExecutor.Enqueue(new AwaitPlayerDeckBuildCommand());
            _deckBuildingCommandExecutor.Enqueue(new DisableInputCommand());
            _deckBuildingCommandExecutor.Enqueue(new AwaitPlayerArrangeDeckCommand());
            _deckBuildingCommandExecutor.Enqueue(new StartGameplayCommand());
        }

        public void AddPlayerDeck(IPlayerDeck playerDeck)
        {
            _playerDecks.Add(playerDeck);
        }
    }
}
