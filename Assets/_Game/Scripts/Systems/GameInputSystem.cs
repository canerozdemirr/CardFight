using System;
using _Game.Scripts.Gameplay.Deck;
using _Game.Scripts.Interfaces.Systems;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public sealed class GameInputSystem : ICardInputSystem, IInitializable, IDisposable, ITickable
    {
        private GameInput _gameInput;
        private Transform draggingCard;
        private bool _isDragging;
        private Camera _mainCamera;
        
        private Vector3 _dragOffset;
        
        public void Initialize()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
            _gameInput.Card.PointerPress.started += OnCardPointerPressed;
            _gameInput.Card.PointerPress.canceled += OnCardPointerPressStopped;
            _mainCamera = Camera.main;
        }
        
        public void Tick()
        {
            if (_isDragging && draggingCard != null)
            {
                Vector2 screenPos = _gameInput.Card.PointerPosition.ReadValue<Vector2>();
                Vector3 worldPos = _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, _mainCamera.nearClipPlane)); // 5f is distance from camera
                draggingCard.position = new Vector3(worldPos.x, worldPos.y, draggingCard.position.z);
            }
        }

        public void Dispose()
        {
            _gameInput.Disable();
            _gameInput.Card.PointerPress.started -= OnCardPointerPressed;
            _gameInput.Card.PointerPress.canceled -= OnCardPointerPressStopped;
        }
        
        private void OnCardPointerPressed(InputAction.CallbackContext obj)
        {
            Vector2 screenPos = _gameInput.Card.PointerPosition.ReadValue<Vector2>();
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, _mainCamera.nearClipPlane));
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null)
            {
                draggingCard = hit.collider.transform;
                _isDragging = true;
                _dragOffset = draggingCard.position - worldPos;
            }
        }
        
        private void OnCardPointerPressStopped(InputAction.CallbackContext obj)
        {
            if (!_isDragging || draggingCard == null)
                return;

            Vector2 screenPos = _gameInput.Card.PointerPosition.ReadValue<Vector2>();
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, _mainCamera.nearClipPlane));
            // RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            draggingCard.position = new Vector3(worldPos.x, worldPos.y, draggingCard.position.z);


            _isDragging = false;
            draggingCard = null;
            
        }
    }
}
