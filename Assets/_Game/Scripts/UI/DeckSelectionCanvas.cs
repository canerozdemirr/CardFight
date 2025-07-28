using System;
using _Game.Scripts.Configs.CardConfigs;
using _Game.Scripts.Configs.PlayerConfigs;
using _Game.Scripts.Events.Deck;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using GenericEventBus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.UI
{
    public class DeckSelectionCanvas : MonoBehaviour
    {
        [Inject] private GenericEventBus<IEvent> _eventBus;

        [Inject] private IPlayerDeck _playerDeck;

        [SerializeField] private Button _playButton;

        private int _currentPlayerCardCount;
        private int _totalPlayerCardCount;

        private void OnEnable()
        {
            _eventBus.SubscribeTo<OnPlayerCardAddedToDeck>(OnPlayerCardAddedToDeck);
            _eventBus.SubscribeTo<OnPlayerCardRemovedFromDeck>(OnPlayerCardRemovedFromDeck);
            _playButton.onClick.AddListener(StartTheGame);
        }

        private void OnDisable()
        {
            _eventBus.UnsubscribeFrom<OnPlayerCardAddedToDeck>(OnPlayerCardAddedToDeck);
            _eventBus.UnsubscribeFrom<OnPlayerCardRemovedFromDeck>(OnPlayerCardRemovedFromDeck);
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
        }
    }
}