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

        public void InitializeVisuals(CardData cardData)
        {
            _nameText.SetText(cardData.CardName);
            _attackText.SetText(cardData.CardAttackData.AttackPoint.ToString());
            _defenseText.SetText(cardData.CardAttackData.DefensePoint.ToString());
        }
    }
    
}
