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
    public class AIDeckController : BaseDeckController, IAIDeck
    {
        [SerializeReference, SubclassSelector] 
        private ICardPickStrategy _cardPickStrategy;

        [SerializeField] 
        private DeckSpot _playingDeckSpot;
        public PlayerTurnData PlayerTurnData => _playerTurnData;
        public CardPlayerHealthData CardPlayerHealthData => _cardPlayerHealthData;

        [Inject] private ICombatRegister _combatRegister;
        
        public bool IsDeckSelected => _cardList.Count >= _cardPlayerData.TotalCardCount;
        public void PrepareDeck()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _combatRegister.RegisterPlayer(this);
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

        public void PlayCard(Cards.Card card)
        {
            _cardList.Remove(card);
        }
    }
}
