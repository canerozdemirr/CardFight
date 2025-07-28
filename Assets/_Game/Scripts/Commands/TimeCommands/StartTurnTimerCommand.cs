using _Game.Scripts.Interfaces.Commands;
using _Game.Scripts.Interfaces.Systems;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Game.Scripts.Commands.TimeCommands
{
    [System.Serializable]
    public class StartTurnTimerCommand : ICommand
    {
        [Inject]
        private ITimeSystem _timeSystem;
        
        private int _turnDurationInSeconds;
        
        public StartTurnTimerCommand(int turnDurationInSeconds)
        {
            _turnDurationInSeconds = turnDurationInSeconds;
        }
        public UniTask Execute()
        {
            _timeSystem.StartTimer(_turnDurationInSeconds);
            return UniTask.CompletedTask;
        }
    }
}