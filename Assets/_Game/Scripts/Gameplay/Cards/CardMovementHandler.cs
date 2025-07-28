using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Cards
{
    public class CardMovementHandler : MonoBehaviour
    {
        [BoxGroup("Card Deck Movement Attributes")] [SerializeField]
        private Ease _deckMovementEaseType = Ease.Linear;

        [BoxGroup("Card Deck Movement Attributes")] [SerializeField]
        private float _deckMovementDuration = 1f;

        [BoxGroup("Card Play Movement Attributes")] [SerializeField]
        private Ease _playingMovementEaseType = Ease.Linear;

        [BoxGroup("Card Play Movement Attributes")] [SerializeField]
        private float _cardPlayMovementDuration = 0.5f;

        private bool _didMoveToDeck;
        public bool DidMoveToDeck => _didMoveToDeck;

        public void MoveCardToDeck(Vector3 targetPosition, bool isRelative = false)
        {
            _didMoveToDeck = true;
            transform.DOMove(targetPosition, _deckMovementDuration)
                .SetEase(_deckMovementEaseType).OnComplete(() => _didMoveToDeck = true)
                .SetRelative(isRelative).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void MoveCardToPlayingDeck(Vector3 targetPosition, bool isRelative = false)
        {
            transform.DOMove(targetPosition, _cardPlayMovementDuration)
                .SetEase(_playingMovementEaseType)
                .SetRelative(isRelative).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}