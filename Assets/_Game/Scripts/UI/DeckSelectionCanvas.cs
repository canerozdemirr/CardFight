using _Game.Scripts.Events.Deck;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.UI;
using GenericEventBus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.UI
{
    using Gameplay.CardPlayers.Data;

    public class DeckSelectionCanvas : MonoBehaviour, IUIElement
    {
        [Inject] private GenericEventBus<IEvent> _eventBus;

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
            _eventBus.SubscribeTo<OnPlayerCardAddedToPlayDeck>(OnPlayerCardAddedToDeck);
            _eventBus.SubscribeTo<OnPlayerCardRemovedFromPlayerDeck>(OnPlayerCardRemovedFromDeck);
            _playButton.onClick.AddListener(StartTheGame);
        }

        public void Cleanup()
        {
            _eventBus.UnsubscribeFrom<OnPlayerCardAddedToPlayDeck>(OnPlayerCardAddedToDeck);
            _eventBus.UnsubscribeFrom<OnPlayerCardRemovedFromPlayerDeck>(OnPlayerCardRemovedFromDeck);
            _playButton.onClick.RemoveAllListeners();
        }

        private void OnPlayerCardRemovedFromDeck(ref OnPlayerCardRemovedFromPlayerDeck eventData)
        {
            if (eventData.PlayerDeck.PlayerOccupation != PlayerOccupation.Player)
                return;

            if (_playButton.gameObject.activeSelf)
                _playButton.gameObject.SetActive(false);
        }

        private void OnPlayerCardAddedToDeck(ref OnPlayerCardAddedToPlayDeck eventData)
        {
            if (eventData.PlayerDeck.PlayerOccupation != PlayerOccupation.Player)
                return;

            if (!eventData.PlayerDeck.IsDeckSelected)
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