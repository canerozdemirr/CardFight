using System;
using _Game.Scripts.Events.Skill;
using _Game.Scripts.Events.Turn;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Interfaces.UI;
using GenericEventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.UI
{
    public class TurnCanvas : MonoBehaviour, IUIElement
    {
        [SerializeField] private Button _endTurnButton;
        [SerializeField] private Button _skillButton;

        [SerializeField] private TextMeshProUGUI _timerText;

        [Inject] private GenericEventBus<IEvent> _eventBus;

        [Inject] private ITimeSystem _timeSystem;
        
        public void Show()
        {
            _endTurnButton.gameObject.SetActive(false);
            _skillButton.gameObject.SetActive(true);
            gameObject.SetActive(true);
            
            _endTurnButton.onClick.AddListener(OnEndTurnButtonClicked);
            _skillButton.onClick.AddListener(OnSkillUsed);
        }

        public void Hide()
        {
            _endTurnButton.onClick.RemoveAllListeners();
            _skillButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }

        public bool IsVisible => gameObject.activeSelf;
        public void Initialize()
        {
            _eventBus.SubscribeTo<OnPlayerCardPlayed>(OnPlayerCardPlayed);
            _eventBus.SubscribeTo<OnPlayerCardRemovedFromDeck>(OnPlayerCardRemovedFromDeck);
            
            _timeSystem.OnSecondElapsed += OnSecondElapsed;
            Hide();
        }

        public void Cleanup()
        {
            _eventBus.UnsubscribeFrom<OnPlayerCardPlayed>(OnPlayerCardPlayed);
            _eventBus.UnsubscribeFrom<OnPlayerCardRemovedFromDeck>(OnPlayerCardRemovedFromDeck);
            
            _timeSystem.OnSecondElapsed -= OnSecondElapsed;
        }

        private void OnSkillUsed()
        {
            _eventBus.Raise(new OnSkillUsed());
            _skillButton.gameObject.SetActive(false);
        }

        private void OnEndTurnButtonClicked()
        {
            _eventBus.Raise(new OnPlayerTurnEnded());
            gameObject.SetActive(false);
        }
        
        private void OnPlayerCardPlayed(ref OnPlayerCardPlayed eventData)
        {
            _endTurnButton.gameObject.SetActive(true);
        }
        
        private void OnPlayerCardRemovedFromDeck(ref OnPlayerCardRemovedFromDeck eventData)
        {
            _endTurnButton.gameObject.SetActive(false);
        }
        
        private void OnSecondElapsed(int secondRemaining)
        {
            _timerText.SetText(secondRemaining.ToString());
        }
    }
}