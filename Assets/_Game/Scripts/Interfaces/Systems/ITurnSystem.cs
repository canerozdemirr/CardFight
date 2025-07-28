using _Game.Scripts.Gameplay.CardPlayers.Data;

namespace _Game.Scripts.Interfaces.Systems
{
    public interface ITurnSystem
    {
        void StartTurn();
        void EndTurn();
        PlayerOccupation CurrentPlayerOccupationToPlay { get; }
    }
}
