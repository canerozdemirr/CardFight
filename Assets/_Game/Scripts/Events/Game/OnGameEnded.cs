using System;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;

namespace _Game.Scripts.Events.Game
{
    public struct OnGameEnded : IEvent
    {
        public ICardPlayer Winner { get; }
        public bool IsDraw { get; }

        public OnGameEnded(ICardPlayer winner)
        {
            Winner = winner;
            IsDraw = winner == null;
        }
    }
}
