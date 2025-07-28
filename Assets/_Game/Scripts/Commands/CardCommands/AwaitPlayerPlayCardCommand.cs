using System;
using _Game.Scripts.Events.Turn;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Events;
using Cysharp.Threading.Tasks;
using GenericEventBus;
using Zenject;

namespace _Game.Scripts.Commands.CardCommands
{
    [Serializable]
    public class AwaitPlayerPlayCardCommand : ICommand
    {
        [Inject]
        private GenericEventBus<IEvent> _eventBus;
        
        private bool _isPlayerCardPlayed;
        
        public async UniTask Execute()
        {
            _isPlayerCardPlayed = false;
            _eventBus.SubscribeTo<OnPlayerTurnEnded>(OnPlayerTurnEnded);
            await UniTask.WaitUntil(() => _isPlayerCardPlayed);
            _eventBus.UnsubscribeFrom<OnPlayerTurnEnded>(OnPlayerTurnEnded);
        }

        private void OnPlayerTurnEnded(ref OnPlayerTurnEnded eventData)
        {
            _isPlayerCardPlayed = true;
        }
    }
}