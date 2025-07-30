using System;
using _Game.Scripts.Commands.CardCommands;
using _Game.Scripts.Commands.InputCommands;
using _Game.Scripts.Commands.TimeCommands;
using _Game.Scripts.Commands.TurnCommands;
using _Game.Scripts.Commands.UICommands;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Utilities.SubSystems;
using GenericEventBus;
using Zenject;

namespace _Game.Scripts.Systems
{
    using Cysharp.Threading.Tasks;

    [Serializable]
    public class TurnSystem : ITurnSystem, IInitializable, IDisposable
    {
        [Inject] 
        private GenericEventBus<IEvent> _eventBus;
        
        [Inject]
        private DiContainer _container;

        [Inject] 
        private ICombatRegister _combatRegister;
        
        [Inject] 
        private ICombatSystem _combatSystem;

        private int _currentTurnCount;
        private int _currentPlayerIndex;
        private int _allPlayersCount;
        
        private ICommandExecutor _playerTurnCommandExecutor;
        private ICommandExecutor _aiTurnCommandExecutor;
        
        public ICardPlayer CurrentCardPlayer => _combatRegister.RegisteredPlayers[_currentPlayerIndex];
        
        public void Initialize()
        {
            _currentTurnCount = UnityEngine.Random.Range(0, 2);
            _playerTurnCommandExecutor = new CommandExecutor(_container);
            _aiTurnCommandExecutor = new CommandExecutor(_container);
            
            FillAITurnCommands();
            FillPlayerPlayTurnCommands();

            _allPlayersCount = _combatRegister.RegisteredPlayers.Count;
        }

        public async UniTask StartTurn()
        {
            _currentPlayerIndex++;
            if (_currentTurnCount == 0)
            {
                await _aiTurnCommandExecutor.ExecuteCommands();
            }
            else
            {
                await _playerTurnCommandExecutor.ExecuteCommands();
            }
            
            if (_currentPlayerIndex >= _allPlayersCount)
            {
                _combatSystem.ResolveCombat();
                _currentPlayerIndex = 0;
            }
        }

        public void EndTurn()
        {
            _currentTurnCount = _currentTurnCount == 0 ? 1 : 0;
            _ = StartTurn();
        }
        
        private void FillPlayerPlayTurnCommands()
        {
            _aiTurnCommandExecutor.Enqueue(new StartTurnTimerCommand(10));
            _playerTurnCommandExecutor.Enqueue(new EnableInputCommand());
            _playerTurnCommandExecutor.Enqueue(new OpenTurnUICommand());
            _playerTurnCommandExecutor.Enqueue(new AwaitPlayerPlayCardCommand());
            _playerTurnCommandExecutor.Enqueue(new DisableInputCommand());
        }
        
        private void FillAITurnCommands()
        {
            _aiTurnCommandExecutor.Enqueue(new StartTurnTimerCommand(10));
            _aiTurnCommandExecutor.Enqueue(new AwaitPlayerPlayCardCommand());
            _aiTurnCommandExecutor.Enqueue(new EndTurnCommand());
        }
        
        public void Dispose()
        {
            
        }
    }
}
