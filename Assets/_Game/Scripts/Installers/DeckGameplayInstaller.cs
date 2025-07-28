using _Game.Scripts.Gameplay.Deck;
using _Game.Scripts.Gameplay.Deck.DeckBuilders;
using _Game.Scripts.Gameplay.Deck.DeckController;
using _Game.Scripts.Interfaces.GameObjects;
using _Game.Scripts.Interfaces.Players;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Installers
{
    public class DeckGameplayInstaller : MonoInstaller
    {
        [BoxGroup("Deck Builders")]
        [SerializeField] private PlayerDeckBuilder playerDeckBuilder;
        
        [BoxGroup("Deck Builders")]
        [SerializeField] private AIDeckBuilder _aiDeckBuilder;
        
        [BoxGroup("Players")]
        [SerializeField] private PlayerDeckController _playerDeck;
        
        [BoxGroup("Players")]
        [SerializeField] private AIDeckController _aiDeck;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AIDeckBuilder>().FromInstance(_aiDeckBuilder).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDeckBuilder>().FromInstance(playerDeckBuilder).AsSingle();
            
            Container.BindInterfacesAndSelfTo<AIDeckController>().FromInstance(_aiDeck).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDeckController>().FromInstance(_playerDeck).AsSingle();
        }
    }
}
