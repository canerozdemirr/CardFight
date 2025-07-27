using _Game.Scripts.Events.Deck;
using _Game.Scripts.Interfaces.Events;
using GenericEventBus;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck.DeckController
{
    public sealed class PlayerDeckController : BaseDeckController
    {
        [Inject] 
        private GenericEventBus<IEvent> _eventBus;
        public override void AddCard(Cards.Card card)
        {
            base.AddCard(card);
            _eventBus.Raise(new OnPlayerCardAddedToDeck());
        }

        public override void RemoveCard(Cards.Card card)
        {
            base.RemoveCard(card);
            _eventBus.Raise(new OnPlayerCardRemovedFromDeck());
        }
    }
}
