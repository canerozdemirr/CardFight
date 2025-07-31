using _Game.Scripts.Interfaces.Events;

namespace _Game.Scripts.Events.Turn
{
    public struct OnPlayerCardRemovedFromPlay : IEvent
    {
        public Gameplay.Cards.Card RemovedCard;
        
        public OnPlayerCardRemovedFromPlay(Gameplay.Cards.Card removedCard)
        {
            RemovedCard = removedCard;
        }
    }
}