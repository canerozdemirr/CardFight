using System;
using _Game.Scripts.Commands.CardCommands;
using _Game.Scripts.Commands.InputCommands;
using _Game.Scripts.Commands.TimeCommands;
using _Game.Scripts.Commands.TurnCommands;
using _Game.Scripts.Commands.UICommands;
using _Game.Scripts.Events.Deck;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.Systems;
using GenericEventBus;
using UnityEngine.UIElements;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public class TurnSystem : ITurnSystem, IInitializable, IDisposable
    {
        [Inject] private GenericEventBus<IEvent> _eventBus;
        
        public PlayerOccupation CurrentPlayerOccupationToPlay { get; }

        [Inject] private IPlayerDeck _playerDeck;
        [Inject] private IAIDeck _aiDeck;

        private int _currentTurnCount;
        
        private ICommandExecutor _playerTurnCommandExecutor;
        private ICommandExecutor _aiTurnCommandExecutor;
        
        public void Initialize()
        {
            _currentTurnCount = UnityEngine.Random.Range(0, 2);
            FillAITurnCommands();
            FillPlayerPlayTurnCommands();
        }

        public void StartTurn()
        {
            if (_currentTurnCount == 0)
            {
                _aiTurnCommandExecutor.ExecuteCommands();
            }
            else
            {
                _playerTurnCommandExecutor.ExecuteCommands();
            }
        }

        public void EndTurn()
        {
            _currentTurnCount = _currentTurnCount == 0 ? 1 : 0;
            StartTurn();
        }
        
        private void FillPlayerPlayTurnCommands()
        {
            _aiTurnCommandExecutor.Enqueue(new StartTurnTimerCommand(_playerDeck.PlayerTurnData.turnDurationInSeconds));
            _playerTurnCommandExecutor.Enqueue(new EnableInputCommand());
            _playerTurnCommandExecutor.Enqueue(new OpenTurnUICommand());
            _playerTurnCommandExecutor.Enqueue(new AwaitPlayerPlayCardCommand());
            _playerTurnCommandExecutor.Enqueue(new DisableInputCommand());
        }
        
        private void FillAITurnCommands()
        {
            _aiTurnCommandExecutor.Enqueue(new StartTurnTimerCommand(_aiDeck.PlayerTurnData.turnDurationInSeconds));
            _aiTurnCommandExecutor.Enqueue(new AICardPickCommand());
            _aiTurnCommandExecutor.Enqueue(new EndTurnCommand());
        }
        
        public void Dispose()
        {
            
        }
    }
}
