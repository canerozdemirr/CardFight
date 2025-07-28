using System;
using _Game.Scripts.Events.Deck;
using _Game.Scripts.Interfaces.Events;
using GenericEventBus;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.UI
{
    public class DeckSelectionCanvas : MonoBehaviour
    {
        [Inject] 
        private GenericEventBus<IEvent> _eventBus;
        
        [SerializeField]
        private Button _playButton;
        
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
            if (!_playButton.gameObject.activeSelf)
                _playButton.gameObject.SetActive(true);
        }

        private void StartTheGame()
        {
            _eventBus.Raise(new OnDeckBuildingEnded());
        }
    }
}
