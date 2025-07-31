using System.Text;
using _Game.Scripts.Gameplay.Card.Data;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Cards
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

        private StringBuilder _textBuilder;

        public void InitializeVisuals(CardData cardData)
        {
            _textBuilder ??= new StringBuilder();
            _textBuilder.Append(cardData.CardName);
            _nameText.SetText(_textBuilder.ToString());
        }

        public void UpdateAttackVisual(int attackPoint)
        {
            _textBuilder.Clear();
            _textBuilder.Append("ATK: ");
            _textBuilder.Append(attackPoint);
            _attackText.SetText(_textBuilder.ToString());
        }

        public void UpdateDefenseVisual(int defensePoint)
        {
            _textBuilder.Clear();
            _textBuilder.Append("DEF: ");
            _textBuilder.Append(defensePoint);
            _defenseText.SetText(_textBuilder.ToString());
        }
    }

}

