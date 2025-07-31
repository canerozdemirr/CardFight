using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Gameplay.Deck.DeckSpots;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.Strategies;
using _Game.Scripts.Interfaces.Systems;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck.DeckController
{
    using Configs.TurnConfigs;

    public class AIDeckController : BaseDeckController
    {
        [SerializeReference, SubclassSelector] 
        private ICardPickStrategy _cardPickStrategy;
        
        [Inject]
        private TurnConfig _turnConfig;

        public override UniTask ArrangeDeck()
        {
            foreach (Cards.Card card in _cardList)
            {
                card.CardDeckCollisionHandler.SetDraggable(false);
            }
            return base.ArrangeDeck();
        }

        public override async UniTask PlayCard()
        {
            int randomSecondDelay = Random.Range(0, 4);
            await UniTask.WaitForSeconds(randomSecondDelay);
            
            Cards.Card pickedCard = _cardPickStrategy.PickACard(_cardList);
            if (pickedCard != null)
            {
                if (pickedCard.TryGetComponent(out CardMovementHandler cardMovementHandler))
                {
                    cardMovementHandler.MoveCardToPlayingDeck(_playingDeckSpot.transform.position);
                    await UniTask.WaitUntil(() => cardMovementHandler.DidMoveToDeck);
                }
                else
                {
                    pickedCard.transform.position = _playingDeckSpot.transform.position;
                }

                _combatSystem.AddCardToCombat(this, pickedCard);
                _cardList.Remove(pickedCard);
            }
            else
            {
                Debug.LogWarning("No card available to play.");
            }
        }
    }
}
