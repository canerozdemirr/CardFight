using System;
using _Game.Scripts.Interfaces.GameObjects;
using _Game.Scripts.Interfaces.Systems;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public sealed class DeckBuildingSystem : IDeckBuildingSystem, IInitializable, IDisposable, ITickable
    {
        [Inject]
        private IPlayerDeckSpawner _playerDeckSpawner;
        
        public void Initialize()
        {
            _playerDeckSpawner.PrepareDeck();
            _playerDeckSpawner.SpawnBeginningDeck();
        }

        public void Dispose()
        {
            
        }

        public void Tick()
        {
            
        }
    }
}
