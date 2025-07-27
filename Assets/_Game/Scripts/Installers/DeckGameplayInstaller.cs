using _Game.Scripts.Gameplay.Deck;
using _Game.Scripts.Gameplay.Deck.DeckBuilders;
using _Game.Scripts.Interfaces.GameObjects;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Installers
{
    public class DeckGameplayInstaller : MonoInstaller
    {
        [SerializeField] private PlayerDeckBuilder playerDeckBuilder;
        [SerializeField] private AIDeckBuilder _aiDeckBuilder;
        public override void InstallBindings()
        {
            Container.Bind<AIDeckBuilder>().FromInstance(_aiDeckBuilder).AsSingle();
            Container.Bind<PlayerDeckBuilder>().FromInstance(playerDeckBuilder).AsSingle();
        }
    }
}
