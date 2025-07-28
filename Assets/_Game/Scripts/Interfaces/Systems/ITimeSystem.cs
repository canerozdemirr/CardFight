using System;

namespace _Game.Scripts.Interfaces.Systems
{
    public interface ITimeSystem
    {
        event Action<int> OnSecondElapsed;
        void StartTimer(int duration);
    }
}