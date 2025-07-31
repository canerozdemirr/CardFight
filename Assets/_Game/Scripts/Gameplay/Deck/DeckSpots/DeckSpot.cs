using UnityEngine;

namespace _Game.Scripts.Gameplay.Deck.DeckSpots
{
    public class DeckSpot : MonoBehaviour
    {
        [SerializeField] private bool _isSpotEmpty = true;
        [SerializeField] private Cards.Card _occupyingCard;
        
        public bool IsSpotEmpty => _isSpotEmpty;
        public Cards.Card OccupyingCard => _occupyingCard;
        public Vector3 Position => transform.position;
        private Collider2D _spotCollider;

        private void Awake()
        {
            _spotCollider = GetComponent<Collider2D>();
            if (_spotCollider == null)
            {
                Debug.LogError($"DeckSpot {name} requires a Collider2D component!");
            }
        }

        public bool TryOccupySpot(Cards.Card card)
        {
            if (!_isSpotEmpty || card == null)
                return false;

            _isSpotEmpty = false;
            _occupyingCard = card;
            return true;
        }

        public void FreeSpot()
        {
            _isSpotEmpty = true;
            _occupyingCard = null;
        }

        public float GetOverlapPercentage(Collider2D cardCollider)
        {
            if (_spotCollider == null || cardCollider == null)
                return 0f;
            
            Bounds spotBounds = _spotCollider.bounds;
            Bounds cardBounds = cardCollider.bounds;

            float xMin = Mathf.Max(spotBounds.min.x, cardBounds.min.x);
            float xMax = Mathf.Min(spotBounds.max.x, cardBounds.max.x);
            float yMin = Mathf.Max(spotBounds.min.y, cardBounds.min.y);
            float yMax = Mathf.Min(spotBounds.max.y, cardBounds.max.y);

            if (xMin >= xMax || yMin >= yMax)
                return 0f;

            float overlapArea = (xMax - xMin) * (yMax - yMin);
            float cardArea = cardBounds.size.x * cardBounds.size.y;
            return cardArea > 0 ? overlapArea / cardArea : 0f;
        }

        public void DisableSpot()
        {
            _spotCollider.enabled = false;
        }

        public void EnableSpot()
        {
            _spotCollider.enabled = true;
        }
    }
}
