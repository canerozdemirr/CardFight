using _Game.Scripts.Gameplay.Deck;
using _Game.Scripts.Interfaces.GameObjects;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Installers
{
    public class DeckGameplayInstaller : MonoInstaller
    {
        [SerializeField] private PlayerDeckBuilder playerDeckBuilder;
        public override void InstallBindings()
        {
            Container.Bind<IPlayerDeckSpawner>().FromInstance(playerDeckBuilder).AsSingle();
        }
    }
}
