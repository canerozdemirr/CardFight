using _Game.Scripts.Interfaces.Events;

namespace _Game.Scripts.Events.Turn
{
    public struct OnPlayerCardPickedToPlay : IEvent
    {
        public readonly Gameplay.Cards.Card PlayedCard;
        
        public OnPlayerCardPickedToPlay(Gameplay.Cards.Card playedCard)
        {
            PlayedCard = playedCard;
        }
    }
}
