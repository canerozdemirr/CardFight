using System.Collections.Generic;
using _Game.Scripts.Interfaces.Players;

namespace _Game.Scripts.Interfaces.Systems
{
    public interface ICombatRegister
    {
        void RegisterPlayer(ICardPlayer player);
        void UnregisterPlayer(ICardPlayer player);

        IReadOnlyList<ICardPlayer> RegisteredPlayers { get; }
    }  
}
