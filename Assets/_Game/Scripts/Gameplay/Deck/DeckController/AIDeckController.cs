using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Gameplay.Deck.DeckSpots;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.Strategies;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Deck.DeckController
{
    public class AIDeckController : BaseDeckController, IAIDeck
    {
        [SerializeReference, SubclassSelector] 
        private ICardPickStrategy _cardPickStrategy;

        [SerializeField] 
        private DeckSpot _playingDeckSpot;
        public PlayerTurnData PlayerTurnData => _playerTurnData;
        public CardPlayerHealthData CardPlayerHealthData => _cardPlayerHealthData;
        
        public bool IsDeckSelected => _cardList.Count >= _cardPlayerData.TotalCardCount;
        public void PrepareDeck()
        {
            Initialize();
        }

        public void AddCardToDeck(Cards.Card card)
        {
            if (!_cardList.Contains(card))
            {
                _cardList.Add(card);
            }
            else
            {
                Debug.LogWarning($"Card {card.name} is already in the deck.");
            }
        }

        public void RemoveCardFromDeck(Cards.Card card)
        {
            if (_cardList.Contains(card))
            {
                _cardList.Remove(card);
            }
            else
            {
                Debug.LogWarning($"Card {card.name} not found in the deck.");
            }
        }

        public async UniTask PlayCard()
        {
            int randomSecondDelay = Random.Range(0, _playerTurnData.turnDurationInSeconds);
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
                
                _cardList.Remove(pickedCard);
            }
            else
            {
                Debug.LogWarning("No card available to play.");
            }
        }
    }
}
