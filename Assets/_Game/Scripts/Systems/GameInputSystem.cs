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
        
        public void Initialize()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
            _gameInput.Card.PointerPress.started += OnCardPointerPressed;
            _gameInput.Card.PointerPress.canceled += OnCardPointerPressStopped;
        }
        
        public void Tick()
        {
            
        }

        public void Dispose()
        {
            _gameInput.Disable();
            _gameInput.Card.PointerPress.started -= OnCardPointerPressed;
            _gameInput.Card.PointerPress.canceled -= OnCardPointerPressStopped;
        }
        
        private void OnCardPointerPressed(InputAction.CallbackContext obj)
        {
            
        }
        
        private void OnCardPointerPressStopped(InputAction.CallbackContext obj)
        {
            
        }
    }
}
