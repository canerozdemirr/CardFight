using _Game.Scripts.Events.Game;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Players;
using _Game.Scripts.Interfaces.UI;
using GenericEventBus;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace _Game.Scripts.UI
{
    public class GameEndCanvas : MonoBehaviour, IUIElement
    {
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private Button _restartButton;

        [Inject] private GenericEventBus<IEvent> _eventBus;

        public void Show()
        {
            gameObject.SetActive(true);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        public void Hide()
        {
            _restartButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }

        public bool IsVisible => gameObject.activeSelf;

        public void Initialize()
        {
            _eventBus.SubscribeTo<OnGameEnded>(OnGameEnded);
            Hide();
        }

        public void Cleanup()
        {
            _eventBus.UnsubscribeFrom<OnGameEnded>(OnGameEnded);
        }

        private void OnDestroy()
        {
            Cleanup();
        }

        private void OnGameEnded(ref OnGameEnded eventData)
        {
            if (eventData.IsDraw)
            {
                _resultText.text = "Game Ended in Draw!";
            }
            else
            {
                string winnerName = eventData.Winner.PlayerOccupation == PlayerOccupation.Player ? "Player" : "AI";
                _resultText.text = $"{winnerName} Won!";
            }
            
            Show();
        }

        private void OnRestartButtonClicked()
        {
            SceneManager.LoadScene(0);
        }
    }
}
