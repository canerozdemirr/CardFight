using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Interfaces.Commands
{
    public interface ICommandExecutor
    {
        void Enqueue(ICommand command);
        
        UniTask ExecuteCommands();
    }
}