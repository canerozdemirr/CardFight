using System;
using System.Collections.Generic;
using _Game.Scripts.Configs.PlayerConfigs;
using _Game.Scripts.Events.Time;
using _Game.Scripts.Gameplay.CardPlayers.Data;
using _Game.Scripts.Interfaces.Events;
using _Game.Scripts.Interfaces.Systems;
using GenericEventBus;
using Zenject;
using UnityEngine;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public class TimeSystem : IInitializable, ITickable, IDisposable, ITimeSystem
    {
        [Inject] private GenericEventBus<IEvent> _eventBus;

        [Inject] private CardPlayerListConfig _cardPlayerListConfig;
        
        private bool _isTimerRunning;
        private float _currentTime;
        private int _timerDuration;
        private float _lastSecondTime;
        
        public event Action<int> OnSecondElapsed;

        private Dictionary<PlayerOccupation, int> _playerTurnDurationList;
        
        public void Initialize()
        {
            _isTimerRunning = false;
            _currentTime = 0f;
            _timerDuration = 0;
            _lastSecondTime = 0f;
            _playerTurnDurationList = new Dictionary<PlayerOccupation, int>();
        }

        public void StartTimer(int duration)
        {
            _isTimerRunning = true;
            _timerDuration = duration;
            _currentTime = 0f;
            _lastSecondTime = 0f;
        }

        private void StopTimer()
        {
            _isTimerRunning = false;
        }
        
        public void Dispose()
        {
            StopTimer();
            OnSecondElapsed = null;
        }

        public void Tick()
        {
            if (!_isTimerRunning) 
                return;
            
            _currentTime += Time.deltaTime;

            if (_currentTime - _lastSecondTime >= 1f)
            {
                _lastSecondTime = Mathf.Floor(_currentTime);
                int remainingTime = Mathf.Max(0, _timerDuration - Mathf.FloorToInt(_currentTime));
                OnSecondElapsed?.Invoke(remainingTime);
            }

            if (!(_currentTime >= _timerDuration)) 
                return;
            
            _isTimerRunning = false;
            _eventBus.Raise(new OnTurnTimePassed());
        }
    }
}
