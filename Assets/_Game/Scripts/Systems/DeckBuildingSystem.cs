using System;
using _Game.Scripts.Commands.DeckCommands;
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
        
        private ICommandExecutor _commandExecutor;
        
        public void Initialize()
        {
            _commandExecutor = new CommandExecutor(_container);
            AddCommands();
        }
        
        public void Dispose()
        {
            
        }

        private void AddCommands()
        {
            _commandExecutor.Enqueue(new StartAIDeckBuildCommand());
            _commandExecutor.Enqueue(new AwaitAIDeckBuildCommand());
            _commandExecutor.Enqueue(new StartPlayerDeckBuildCommand());
            _commandExecutor.Enqueue(new AwaitPlayerDeckBuildCommand());
        }
    }
}
