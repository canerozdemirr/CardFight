using _Game.Scripts.Interfaces.Events;

namespace _Game.Scripts.Events.Time
{
    public struct OnTurnTimePassed : IEvent
    {
        public int RemainingSeconds;

        public OnTurnTimePassed(int remainingSeconds)
        {
            RemainingSeconds = remainingSeconds;
        }
    }
}