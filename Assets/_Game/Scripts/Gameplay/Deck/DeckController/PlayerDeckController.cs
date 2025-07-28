using _Game.Scripts.Events.Deck;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using GenericEventBus;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck.DeckController
{
    public sealed class PlayerDeckController : BaseDeckController, IPlayerDeck
    {
        [Inject] 
        private GenericEventBus<IEvent> _eventBus;
        public bool IsDeckSelected => _cardList.Count == _totalCardCount;
        
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
