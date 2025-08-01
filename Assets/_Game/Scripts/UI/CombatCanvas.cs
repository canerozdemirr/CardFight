namespace _Game.Scripts.UI
{
    using Gameplay.CardPlayers.Data;
    using Interfaces.Players;
    using Interfaces.Systems;
    using Interfaces.UI;
    using TMPro;
    using UnityEngine;
    using Zenject;

    public class CombatCanvas : MonoBehaviour, IUIElement
    {
        [SerializeField]
        private TextMeshProUGUI _playerHPText;
        
        [SerializeField]
        private TextMeshProUGUI _opponentHPText;
        
        [Inject] private ICombatRegister _combatRegister;
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
            foreach (ICardPlayer cardPlayer in _combatRegister.RegisteredPlayers)
            {
                if (cardPlayer.PlayerOccupation == PlayerOccupation.Player)
                {
                    cardPlayer.Health.OnHealthChanged += OnPlayerHealthChanged;
                    _playerHPText.SetText($"Player HP: {cardPlayer.Health.CurrentHealth}");
                }
                else
                {
                    cardPlayer.Health.OnHealthChanged += OnOpponentHealthChanged;
                    _opponentHPText.SetText($"Opponent HP: {cardPlayer.Health.CurrentHealth}");
                }
            }
            Hide();
        }

        private void OnOpponentHealthChanged(int currentHealth, int maxHealth)
        {
            _opponentHPText.SetText($"Opponent HP: {currentHealth}");
        }

        private void OnPlayerHealthChanged(int currentHealth, int maxHealth)
        {
            _playerHPText.SetText($"Player HP: {currentHealth}");
        }

        public void Cleanup()
        {
            foreach (ICardPlayer cardPlayer in _combatRegister.RegisteredPlayers)
            {
                if (cardPlayer.PlayerOccupation == PlayerOccupation.Player)
                {
                    cardPlayer.Health.OnHealthChanged -= OnPlayerHealthChanged;
                }
                else
                {
                    cardPlayer.Health.OnHealthChanged -= OnOpponentHealthChanged;
                }
            }
        }
    }
}