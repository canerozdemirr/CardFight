using System;
using _Game.Scripts.Events.Deck;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using Cysharp.Threading.Tasks;
using GenericEventBus;
using Zenject;

namespace _Game.Scripts.Commands.DeckCommands
{
    [Serializable]
    public class AwaitPlayerDeckBuildCommand : ICommand
    {
        [Inject]
        private IPlayerDeck _playerDeck;
        
        [Inject]
        private GenericEventBus<IEvent> _eventBus;
        
        private bool _isDeckBuildingEnded;
        
        public async UniTask Execute()
        {
            _isDeckBuildingEnded = false;
            _eventBus.SubscribeTo<OnDeckBuildingEnded>(OnDeckBuildingEnded);
            await UniTask.WaitUntil(() => _isDeckBuildingEnded);
            _eventBus.UnsubscribeFrom<OnDeckBuildingEnded>(OnDeckBuildingEnded);
        }

        private void OnDeckBuildingEnded(ref OnDeckBuildingEnded eventData)
        {
            _isDeckBuildingEnded = true;
        }
    }
}