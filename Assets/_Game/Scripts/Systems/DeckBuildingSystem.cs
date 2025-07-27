using System;
using _Game.Scripts.Gameplay.Deck.DeckBuilders;
using _Game.Scripts.Interfaces.GameObjects;
using _Game.Scripts.Interfaces.Systems;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public sealed class DeckBuildingSystem : IDeckBuildingSystem, IInitializable, IDisposable, ITickable
    {
        [Inject]
        private PlayerDeckBuilder _playerDeckBuilder;
        
        [Inject]
        private AIDeckBuilder _aiDeckBuilder;
        
        public void Initialize()
        {
            _aiDeckBuilder.PrepareDeck();
            _playerDeckBuilder.PrepareDeck();
        }

        public void Tick()
        {
            
        }
        
        public void Dispose()
        {
            
        }
    }
}
