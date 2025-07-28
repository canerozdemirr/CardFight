using _Game.Scripts.Interfaces.Events;

namespace _Game.Scripts.Events.Turn
{
    public struct OnPlayerCardPlayed : IEvent
    {
        public readonly Gameplay.Cards.Card PlayedCard;
        
        public OnPlayerCardPlayed(Gameplay.Cards.Card playedCard)
        {
            PlayedCard = playedCard;
        }
    }
}
