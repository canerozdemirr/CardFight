using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Interfaces.Commands
{
    public interface ICommand
    {
        UniTask Execute();
    }
}
