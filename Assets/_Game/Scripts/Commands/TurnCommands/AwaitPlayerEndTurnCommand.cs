using System;
using _Game.Scripts.Events.Turn;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Events;
using Cysharp.Threading.Tasks;
using GenericEventBus;
using Zenject;

namespace _Game.Scripts.Commands.TurnCommands
{
    [Serializable]
    public class AwaitPlayerEndTurnCommand : ICommand
    {
        [Inject] private GenericEventBus<IEvent> _eventBus;
        
        private bool _didPlayerEndTurn;
        public async UniTask Execute()
        {
            _didPlayerEndTurn = false;
            _eventBus.SubscribeTo<OnPlayerTurnEnded>(OnPlayerEndTurn);
            await UniTask.WaitUntil(() => _didPlayerEndTurn);
            _eventBus.UnsubscribeFrom<OnPlayerTurnEnded>(OnPlayerEndTurn);
        }

        private void OnPlayerEndTurn(ref OnPlayerTurnEnded eventData)
        {
            _didPlayerEndTurn = true;
        }
    }
}