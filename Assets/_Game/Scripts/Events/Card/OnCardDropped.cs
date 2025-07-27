using _Game.Scripts.Interfaces.Events;

namespace _Game.Scripts.Events.Card
{
    public struct OnCardDropped : IEvent
    {
        public readonly Gameplay.Cards.Card PickedCard;

        public OnCardDropped(Gameplay.Cards.Card pickedCard)
        {
            PickedCard = pickedCard;
        }
    }
}
