using System.Collections.Generic;
using System.Threading;
using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Configs.PlayerConfigs;
using _Game.Scripts.Gameplay.Card.Data;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Interfaces.Deck;
using _Game.Scripts.Interfaces.Players;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Gameplay.Deck.DeckBuilders
{
    public class AIDeckBuilder : BaseDeckBuilder, IAIDeckBuilder
    {
        [SerializeField]
        private Transform _spawnPoint;

        [Inject] 
        private CardListConfig _cardListConfig;

        private List<CardData> _unselectedCardDatas = new();

        private CancellationTokenSource _cancellationTokenSource;

        [Inject] 
        protected IAIDeck _aiDeck;

        public void PrepareAIDeck()
        {
            _unselectedCardDatas = new List<CardData>(_cardListConfig.CardConfigList.Length);
            
            foreach (CardConfig cardConfig in _cardListConfig.CardConfigList)
            {
                _unselectedCardDatas.Add(cardConfig.CardData);
            }
            _aiDeck.PrepareDeck();
            StartPickingCards();
        }
        
        private void StartPickingCards()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _ = PickCardsTask(_cancellationTokenSource.Token);
        }
        
        private async UniTask PickCardsTask(CancellationToken cancellationToken)
        {
            while (!_aiDeck.IsDeckSelected)
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                int randomIndex = Random.Range(0, _unselectedCardDatas.Count - 1);
                CardData selectedCardData = _unselectedCardDatas[randomIndex];
                Cards.Card spawnedCard = _cardFactory.Create(selectedCardData);
                _aiDeck.AddCardToDeck(spawnedCard);
                spawnedCard.transform.position = _spawnPoint.position;
                
                await UniTask.WaitForEndOfFrame(cancellationToken);
            }
        }
        
        private void OnDisable()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}
