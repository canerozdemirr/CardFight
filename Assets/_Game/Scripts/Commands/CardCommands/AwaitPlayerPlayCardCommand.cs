using System;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Events;
using Cysharp.Threading.Tasks;
using GenericEventBus;
using Zenject;

namespace _Game.Scripts.Commands.CardCommands
{
    using Gameplay.Cards;
    using Interfaces.Players;
    using Interfaces.Systems;

    [Serializable]
    public class AwaitPlayerPlayCardCommand : ICommand
    {
        [Inject] private GenericEventBus<IEvent> _eventBus;

        [Inject] private ITurnSystem _turnSystem;

        private ICardPlayer _cardPlayer;

        public async UniTask Execute()
        {
            _cardPlayer = _turnSystem.CurrentCardPlayer;
            await _cardPlayer.PlayCard();
        }
    }
}