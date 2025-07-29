using System;
using _Game.Scripts.Gameplay.Cards;
using _Game.Scripts.Interfaces.Systems;
using Zenject;

namespace _Game.Scripts.Systems
{
    [Serializable]
    public class CombatSystem : ICombatSystem, IInitializable, IDisposable
    {
        public void ResolveCombat(Card playerCard, Card botCard)
        {

        }

        public void Initialize()
        {

        }

        public void Dispose()
        {

        }
    }
}
