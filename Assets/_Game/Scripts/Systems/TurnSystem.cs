using System;
using _Game.Scripts.Events.Deck;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Systems;
using GenericEventBus;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public class TurnSystem : ITurnSystem, IInitializable, IDisposable, ITickable
    {
        [Inject] private GenericEventBus<IEvent> _eventBus;
        
        public PlayerOccupation CurrentPlayerOccupation { get; }
        
        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }

        public void Tick()
        {
            
        }
        
        private void OnDeckBuildingEnded(ref OnDeckBuildingEnded eventData)
        {
            
        }

        public void StartTurn()
        {
            
        }

        public void EndTurn()
        {
            
        }
    }
}
