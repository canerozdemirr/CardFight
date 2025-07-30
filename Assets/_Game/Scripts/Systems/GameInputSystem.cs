using System;
using _Game.Scripts.Events.Card;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Systems;
using _Game.Scripts.Utilities;
using GenericEventBus;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public sealed class GameInputSystem : ICardInputSystem, IInitializable, IDisposable, ITickable
    {
        private GameInput _gameInput;
        private Card _draggingCard;
        private bool _isDragging;
        private Camera _mainCamera;

        private LayerMask _cardLayer;

        private readonly float _inputRadius = 0.5f;
        
        [Inject]
        private GenericEventBus<IEvent> _eventBus;
        
        public void Initialize()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
            _gameInput.Card.PointerPress.started += OnCardPointerPressed;
            _gameInput.Card.PointerPress.canceled += OnCardPointerPressStopped;
            _mainCamera = Camera.main;
            _cardLayer = 1 << LayerMask.NameToLayer(Constants.CardLayerTag);
        }
        
        public void Tick()
        {
            if (!_isDragging || _draggingCard == null) 
                return;
            
            Vector2 screenPos = _gameInput.Card.PointerPosition.ReadValue<Vector2>();
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, -_mainCamera.transform.position.z));
            _draggingCard.transform.position = new Vector2(worldPos.x, worldPos.y);
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
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, -_mainCamera.transform.position.z));
            
            RaycastHit2D hit = Physics2D.CircleCast(worldPos, _inputRadius, Vector2.zero, 0f, _cardLayer);

            if (hit.collider == null || !hit.collider.TryGetComponent(out Card pickedCard)) 
                return;
            
            _draggingCard = pickedCard;
            _isDragging = true;
        }
        
        private void OnCardPointerPressStopped(InputAction.CallbackContext obj)
        {
            if (!_isDragging || _draggingCard == null)
                return;
            
            _eventBus.Raise(new OnCardDropped(_draggingCard));
            _isDragging = false;
            _draggingCard = null;
        }

        public void OpenInput()
        {
            _gameInput.Enable();
        }

        public void CloseInput()
        {
            _gameInput.Disable();
        }
    }
}
