using System.Collections.Generic;
using _Game.Scripts.Configs.PlayerConfigs;
using _Game.Scripts.Events.Deck;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using Cysharp.Threading.Tasks;
using GenericEventBus;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck.DeckController
{
    public sealed class PlayerDeckController : BaseDeckController, IPlayerDeck
    {
        [Inject] private GenericEventBus<IEvent> _eventBus;
        public PlayerTurnData PlayerTurnData => _playerTurnData;
        public CardPlayerHealthData CardPlayerHealthData => _cardPlayerHealthData;
        public bool IsDeckSelected => _cardList.Count == _cardPlayerData.TotalCardCount;

        public void PrepareDeck()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public void AddCard(Cards.Card card)
        {
            _cardList.Add(card);
            _eventBus.Raise(new OnPlayerCardAddedToDeck());
        }

        public void RemoveCard(Cards.Card card)
        {
            _cardList.Remove(card);
            _eventBus.Raise(new OnPlayerCardRemovedFromDeck());
        }

        public async UniTask ArrangeDeck()
        {
            for (int i = 0; i < _cardList.Count; i++)
            {
                Cards.Card pickedCard = _cardList[i];
                if (pickedCard.TryGetComponent(out CardMovementHandler cardMovementHandler))
                {
                    cardMovementHandler.MoveCardToDeck(_deckPoints.Length > i
                        ? _deckPoints[i].position
                        : _deckPoints[^1].position);
                    await UniTask.WaitUntil(() => cardMovementHandler.DidMoveToDeck);
                }
                else
                {
                    pickedCard.transform.position =
                        _deckPoints.Length > i ? _deckPoints[i].position : _deckPoints[^1].position;
                }
            }
        }

        public void PlayCard(Cards.Card card)
        {
            _cardList.Remove(card);
        }
    }
}