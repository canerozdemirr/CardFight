using _Game.Scripts.Gameplay.Deck;
using _Game.Scripts.Interfaces.GameObjects;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Installers
{
    public class DeckGameplayInstaller : MonoInstaller
    {
        [SerializeField] private PlayerDeckSpawner _playerDeckSpawner;
        public override void InstallBindings()
        {
            Container.Bind<IPlayerDeckSpawner>().FromInstance(_playerDeckSpawner).AsSingle();
        }
    }
}
