using _Game.Scripts.Gameplay.CardPlayers.Data;

namespace _Game.Scripts.Interfaces.Systems
{
    using Cysharp.Threading.Tasks;
    using Players;

    public interface ITurnSystem
    {
        UniTask StartTurn();
        void EndTurn();
        ICardPlayer CurrentCardPlayer { get; }
    }
}
