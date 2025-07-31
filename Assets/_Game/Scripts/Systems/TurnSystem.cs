using System;
using System.Collections.Generic;
using _Game.Scripts.Commands.CardCommands;
using _Game.Scripts.Commands.InputCommands;
using _Game.Scripts.Commands.TimeCommands;
using _Game.Scripts.Commands.TurnCommands;
using _Game.Scripts.Commands.UICommands;
using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.UI;
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
        
        private List<ICommand> _playerTurnCommands = new();
        private List<ICommand> _aiTurnCommands = new();

        private ICardPlayer _cardPlayer;
        public ICardPlayer CurrentCardPlayer => _cardPlayer;
        
        public void Initialize()
        {
            _playerTurnCommandExecutor = new CommandExecutor(_container);
            _aiTurnCommandExecutor = new CommandExecutor(_container);
            
            CreateAITurnCommands();
            CreatePlayerPlayTurnCommands();
            
            FillPlayerTurnCommands();
            FillAITurnCommands();

            _allPlayersCount = _combatRegister.RegisteredPlayers.Count;
        }

        public async UniTask StartTurn()
        {
            ICardPlayer cardPlayer = _combatRegister.RegisteredPlayers[_currentPlayerIndex];
            _cardPlayer = cardPlayer;
            
            if (cardPlayer.PlayerOccupation == PlayerOccupation.Player)
            {
                _skillSystem.ApplyRandomSkill(cardPlayer);
            }
            
            if (cardPlayer.PlayerOccupation == PlayerOccupation.Bot)
            {
                await _aiTurnCommandExecutor.ExecuteCommands();
                _currentPlayerIndex++;
                FillAITurnCommands();
            }
            else
            {
                await _playerTurnCommandExecutor.ExecuteCommands();
                _currentPlayerIndex++;
                FillPlayerTurnCommands();
            }
            
            if (_currentPlayerIndex >= _allPlayersCount)
            {
                if (_currentTurnCount >= _turnConfig.MaxTurnsBeforeGameEnd)
                {
                    //GAME IS FINISHED!
                    return;
                }

                _currentTurnCount++;
                _combatSystem.ResolveCombat();
                _currentPlayerIndex = 0;
            }
            _ = StartTurn();
        }

        public void EndTurn()
        {
            
        }
        
        private void CreatePlayerPlayTurnCommands()
        {
            _playerTurnCommands.Add(new StartTurnTimerCommand(_turnConfig.TurnDurationInSeconds));
            _playerTurnCommands.Add(new EnableInputCommand());
            _playerTurnCommands.Add(new OpenUICommand<TurnCanvas>());
            _playerTurnCommands.Add(new OpenUICommand<CombatCanvas>());
            _playerTurnCommands.Add(new OpenUICommand<SkillCanvas>());
            _playerTurnCommands.Add(new AwaitPlayerEndTurnCommand());
            _playerTurnCommands.Add(new DisableInputCommand());
            _playerTurnCommands.Add(new CloseUICommand<TurnCanvas>());
            _playerTurnCommands.Add(new CloseUICommand<CombatCanvas>());
            _playerTurnCommands.Add(new CloseUICommand<SkillCanvas>());
        }
        
        private void CreateAITurnCommands()
        {
            _aiTurnCommands.Add(new StartTurnTimerCommand(_turnConfig.TurnDurationInSeconds));
            _aiTurnCommands.Add(new AwaitPlayerPlayCardCommand());
        }
        
        public void FillPlayerTurnCommands()
        {
            foreach (ICommand player in _playerTurnCommands)
            {
                _playerTurnCommandExecutor.Enqueue(player);
            }
        }
        
        public void FillAITurnCommands()
        {
            foreach (ICommand ai in _aiTurnCommands)
            {
                _aiTurnCommandExecutor.Enqueue(ai);
            }
        }
        
        public void Dispose()
        {
            
        }
    }
}
