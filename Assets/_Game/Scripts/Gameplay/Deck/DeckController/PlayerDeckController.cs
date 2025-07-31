using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Systems;
using Cysharp.Threading.Tasks;
using GenericEventBus;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck.DeckController
{
    using DeckSpots;
    using Events.Turn;
    using Card = Cards.Card;

    public sealed class PlayerDeckController : BaseDeckController
    {
        [Inject] private GenericEventBus<IEvent> _eventBus;

        [Inject] private ICombatSystem _combatSystem;

        private Card _pickedCardToPlay;
        private bool _isCardPickedToPlay;

        protected override void Initialize()
        {
            base.Initialize();
            _eventBus.SubscribeTo<OnPlayerCardPickedToPlay>(OnPlayerCardPickedToPlay);
            _eventBus.SubscribeTo<OnPlayerCardRemovedFromPlay>(OnPlayerCardRemovedFromPlay);
            _eventBus.SubscribeTo<OnPlayerTurnEnded>(OnPlayerTurnEnded);
        }

        public override async UniTask ArrangeDeck()
        {
            for (int i = 0; i < _cardList.Count; i++)
            {
                Card pickedCard = _cardList[i];
                DeckSpot deckSpot = _deckPoints.Length > i ? _deckPoints[i] : _deckPoints[^1];
                if (pickedCard.TryGetComponent(out CardMovementHandler cardMovementHandler))
                {
                    cardMovementHandler.MoveCardToDeck(deckSpot.transform.position);
                    pickedCard.CardDeckCollisionHandler.OverrideDefaultDeckSpot(deckSpot);
                    await UniTask.WaitUntil(() => cardMovementHandler.DidMoveToDeck);
                }
                else
                {
                    pickedCard.transform.position = deckSpot.transform.position;
                }
            }
        }

        private void OnDisable()
        {
            _eventBus.UnsubscribeFrom<OnPlayerCardPickedToPlay>(OnPlayerCardPickedToPlay);
            _eventBus.UnsubscribeFrom<OnPlayerCardRemovedFromPlay>(OnPlayerCardRemovedFromPlay);
            _eventBus.UnsubscribeFrom<OnPlayerTurnEnded>(OnPlayerTurnEnded);
        }

        public override async UniTask PlayCard()
        {
            await UniTask.WaitUntil(() => _isCardPickedToPlay);
            _combatSystem.AddCardToCombat(this, _pickedCardToPlay);
            _cardList.Remove(_pickedCardToPlay);
        }
        
        private void OnPlayerCardPickedToPlay(ref OnPlayerCardPickedToPlay eventData)
        {
            _pickedCardToPlay = eventData.PlayedCard;
            _combatSystem.AddCardToCombat(this, _pickedCardToPlay);
        }
        
        private void OnPlayerCardRemovedFromPlay(ref OnPlayerCardRemovedFromPlay eventData)
        {
            _pickedCardToPlay = null;
            _combatSystem.RemoveCardFromCombat(this);
        }
        
        private void OnPlayerTurnEnded(ref OnPlayerTurnEnded eventData)
        {
            _isCardPickedToPlay = true;
        }
    }
}