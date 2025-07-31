using System.Text;
using _Game.Scripts.Gameplay.Card.Data;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Card
{
    public class CardVisualHandler : MonoBehaviour
    {
        [BoxGroup("Card Texts")] 
        [SerializeField]
        private TextMeshPro _nameText;
        
        [BoxGroup("Card Texts")]
        [SerializeField]
        private TextMeshPro _attackText;
        
        [BoxGroup("Card Texts")]
        [SerializeField]
        private TextMeshPro _defenseText;
        
        [BoxGroup("Card Texts")]
        [SerializeField]
        private TextMeshPro _healthText;

        private StringBuilder _textBuilder;

        public void InitializeVisuals(CardData cardData)
        {
            _textBuilder ??= new StringBuilder();
            _textBuilder.Append(cardData.CardName);
            _nameText.SetText(_textBuilder.ToString());
            _textBuilder.Clear();

            _textBuilder.Append("ATK: ");
            _textBuilder.Append(cardData.CardAttackData.AttackPoint);
            _attackText.SetText(_textBuilder.ToString());
            _textBuilder.Clear();
            
            _textBuilder.Append("DEF: ");
            _textBuilder.Append(cardData.CardAttackData.DefensePoint);
            _defenseText.SetText(_textBuilder.ToString());
            _textBuilder.Clear();
            
            _textBuilder.Append("HP: ");
            _textBuilder.Append(cardData.CardAttackData.Health);
            _healthText.SetText(_textBuilder.ToString());
            _textBuilder.Clear();
        }
        
        public void UpdateAttackPointDisplay(int attackPoint)
        {
            _textBuilder ??= new StringBuilder();
            _textBuilder.Append("ATK: ");
            _textBuilder.Append(attackPoint);
            _attackText.SetText(_textBuilder.ToString());
            _textBuilder.Clear();
        }
        
        public void UpdateDefensePointDisplay(int defensePoint)
        {
            _textBuilder ??= new StringBuilder();
            _textBuilder.Append("DEF: ");
            _textBuilder.Append(defensePoint);
            _defenseText.SetText(_textBuilder.ToString());
            _textBuilder.Clear();
        }
        
        public void UpdateHealthPointDisplay(int currentHealth, int maxHealth)
        {
            _textBuilder ??= new StringBuilder();
            _textBuilder.Append("HP: ");
            _textBuilder.Append(currentHealth);
            _textBuilder.Append("/");
            _textBuilder.Append(maxHealth);
            _healthText.SetText(_textBuilder.ToString());
            _textBuilder.Clear();
        }
        
        public void UpdateAllStatsDisplay(int attackPoint, int defensePoint, int currentHealth, int maxHealth)
        {
            UpdateAttackPointDisplay(attackPoint);
            UpdateDefensePointDisplay(defensePoint);
            UpdateHealthPointDisplay(currentHealth, maxHealth);
        }
    }
    
}
