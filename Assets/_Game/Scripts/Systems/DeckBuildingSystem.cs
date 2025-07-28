using System;
using _Game.Scripts.Commands.DeckCommands;
using _Game.Scripts.Commands.InputCommands;
using _Game.Scripts.Gameplay.Deck.DeckBuilders;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.GameObjects;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Utilities.SubSystems;
using Zenject;

namespace _Game.Scripts.Systems
{
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
        
        public void Initialize()
        {
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
            _deckBuildingCommandExecutor.Enqueue(new AwaitAIDeckBuildCommand());
            _deckBuildingCommandExecutor.Enqueue(new StartPlayerDeckBuildCommand());
            _deckBuildingCommandExecutor.Enqueue(new AwaitPlayerDeckBuildCommand());
            _deckBuildingCommandExecutor.Enqueue(new DisableInputCommand());
            _deckBuildingCommandExecutor.Enqueue(new AwaitPlayerArrangeDeckCommand());
        }
    }
}
