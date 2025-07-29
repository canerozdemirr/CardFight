using System;
using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Configs.PlayerConfigs;
using _Game.Scripts.Events.Deck;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.UI;
using GenericEventBus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.UI
{
    public class DeckSelectionCanvas : MonoBehaviour, IUIElement
    {
        [Inject] private GenericEventBus<IEvent> _eventBus;

        [Inject] private IPlayerDeck _playerDeck;

        [SerializeField] private Button _playButton;

        [SerializeField] private GameObject _playDeck;

        private int _currentPlayerCardCount;
        private int _totalPlayerCardCount;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public bool IsVisible => gameObject.activeSelf;
        public void Initialize()
        {
            _eventBus.SubscribeTo<OnPlayerCardAddedToDeck>(OnPlayerCardAddedToDeck);
            _eventBus.SubscribeTo<OnPlayerCardRemovedFromDeck>(OnPlayerCardRemovedFromDeck);
            _playButton.onClick.AddListener(StartTheGame);
        }

        public void Cleanup()
        {
            _eventBus.UnsubscribeFrom<OnPlayerCardAddedToDeck>(OnPlayerCardAddedToDeck);
            _eventBus.UnsubscribeFrom<OnPlayerCardRemovedFromDeck>(OnPlayerCardRemovedFromDeck);
            _playButton.onClick.RemoveAllListeners();
        }

        private void OnPlayerCardRemovedFromDeck(ref OnPlayerCardRemovedFromDeck eventData)
        {
            if (_playButton.gameObject.activeSelf)
                _playButton.gameObject.SetActive(false);
        }

        private void OnPlayerCardAddedToDeck(ref OnPlayerCardAddedToDeck eventData)
        {
            if (!_playerDeck.IsDeckSelected)
                return;

            if (!_playButton.gameObject.activeSelf)
                _playButton.gameObject.SetActive(true);
        }

        private void StartTheGame()
        {
            _eventBus.Raise(new OnDeckBuildingEnded());
            _playButton.gameObject.SetActive(false);
            _playDeck.gameObject.SetActive(true);
        }

        
    }
}