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
    using Configs.TurnConfigs;
    using Cysharp.Threading.Tasks;
    using Gameplay.CardPlayers.Data;

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
        
        [Inject] 
        private TurnConfig _turnConfig;
        
        [Inject]
        private ISkillSystem _skillSystem;

        private int _currentTurnCount;
        private int _currentPlayerIndex;
        private int _allPlayersCount;
        
        private ICommandExecutor _playerTurnCommandExecutor;
        private ICommandExecutor _aiTurnCommandExecutor;
        
        public ICardPlayer CurrentCardPlayer => _combatRegister.RegisteredPlayers[_currentPlayerIndex];
        
        public void Initialize()
        {
            _playerTurnCommandExecutor = new CommandExecutor(_container);
            _aiTurnCommandExecutor = new CommandExecutor(_container);
            
            FillAITurnCommands();
            FillPlayerPlayTurnCommands();

            _allPlayersCount = _combatRegister.RegisteredPlayers.Count;
        }

        public async UniTask StartTurn()
        {
            ICardPlayer cardPlayer = _combatRegister.RegisteredPlayers[_currentPlayerIndex];
            _currentTurnCount++;
            
            // Apply random skill when it's a player's turn (not AI/Bot)
            if (cardPlayer.PlayerOccupation == PlayerOccupation.Player)
            {
                _skillSystem?.ApplyRandomSkill();
            }
            
            if (cardPlayer.PlayerOccupation == PlayerOccupation.Bot)
            {
                await _aiTurnCommandExecutor.ExecuteCommands();
            }
            else
            {
                await _playerTurnCommandExecutor.ExecuteCommands();
            }
            
            if (_currentPlayerIndex >= _allPlayersCount)
            {
                if (_currentTurnCount >= _turnConfig.MaxTurnsBeforeGameEnd)
                {
                    //GAME IS FINISHED!
                    return;
                }

                _combatSystem.ResolveCombat();
                _currentPlayerIndex = 0;
            }
            else
            {
                _currentPlayerIndex++;
            }

            _ = StartTurn();
        }

        public void EndTurn()
        {
            _ = StartTurn();
        }
        
        private void FillPlayerPlayTurnCommands()
        {
            _aiTurnCommandExecutor.Enqueue(new StartTurnTimerCommand(_turnConfig.TurnDurationInSeconds));
            _playerTurnCommandExecutor.Enqueue(new EnableInputCommand());
            _playerTurnCommandExecutor.Enqueue(new OpenTurnUICommand());
            _playerTurnCommandExecutor.Enqueue(new OpenCombatUICommand());
            _playerTurnCommandExecutor.Enqueue(new AwaitPlayerPlayCardCommand());
            _playerTurnCommandExecutor.Enqueue(new DisableInputCommand());
        }
        
        private void FillAITurnCommands()
        {
            _aiTurnCommandExecutor.Enqueue(new StartTurnTimerCommand(_turnConfig.TurnDurationInSeconds));
            _aiTurnCommandExecutor.Enqueue(new AwaitPlayerPlayCardCommand());
        }
        
        public void Dispose()
        {
            
        }
    }
}
